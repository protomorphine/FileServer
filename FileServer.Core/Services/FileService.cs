using FileServer.Core.Services.Interfaces;
using FileServer.Core.Repositories;
using FileServer.Core.Extensions;
using FileServer.Core.Models;
using FileServer.Core.Managers;
using FileServer.Core.Dtos;
using FileServer.Core.Entities;

namespace FileServer.Core.Services
{
    /// <summary>
    /// Сервис для работы с файлами
    /// </summary>
    public class FileService : IFileService
    {
        #region поля

        /// <summary>
        /// Настройки хранилища
        /// </summary>
        private readonly StorageOptions _storageOptions;
        
        /// <summary>
        /// Репозиторий для работы с файлами
        /// </summary>
        private readonly IFileRepository _fileRepository;

        /// <summary>
        /// Менеджер для работы с транзакциями базы данных
        /// </summary>
        private readonly IDbTransactionManager _dbTrancactionManager;

        #endregion

        #region конструкторы

        public FileService(
            StorageOptions storageOptions, 
            IFileRepository fileRepository,
            IDbTransactionManager dbTransactionManager)
        {
            _storageOptions = storageOptions;
            _fileRepository = fileRepository;
            _dbTrancactionManager = dbTransactionManager;
        }

        #endregion

        #region методы

        /// <summary>
        /// Метод загрузки файла на сервер
        /// </summary>
        /// <param name="file">Поток, получаемый из IFormFile</param>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Id файла в формате Guid</returns>
        public async Task<Guid> Upload(Stream file, string fileName)
        {
            await using var dbTransaction = await _dbTrancactionManager.BeginTransactionAsync();
            try
            {
                var fileEntity = await AddToDbAsync(fileName);
                
                await CopyFileAsync(file, fileEntity.Id.ToString());

                await dbTransaction.CommitAsync();
                
                return fileEntity.Id;
            }
            catch
            {
                await dbTransaction.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Получает файл из папки, указанной в appsettings.json
        /// </summary>
        /// <param name="id">id файла в формате Guid</param>
        public async Task<FileResponceModel> Download(Guid id)
        {
            var file = await _fileRepository.GetAsync(id);

            file.ThrowIfNotFound("Файл не найден");

            var filePath = Path.Combine(_storageOptions.FileDir!, file!.Id.ToString());

            if (!File.Exists(filePath))
                throw new FileNotFoundException("Запрашиваемый файл не найден!");
           
            byte[] bytesFromFile = await File.ReadAllBytesAsync(filePath);

            return new FileResponceModel {
                Name = file.Name,
                ContentType = "application/octet-stream",
                FileContent = bytesFromFile
            };
        }

        /// <summary>
        /// Удаляет файл из папки, указанной в appsettings.json
        /// </summary>
        /// <param name="id">Имя файла</param>
        public async Task Delete(Guid id)
        {
            var file = await _fileRepository.GetAsync(id);
            file.ThrowIfNotFound("Файл не найден.");
            await _fileRepository.DeleteAsync(file!);

            var filePath = Path.Combine(_storageOptions?.FileDir!, file!.Id.ToString());

            if (!File.Exists(filePath))
                throw new FileNotFoundException("Файл не найден!");

            await Task.Run(() => File.Delete(filePath));
        }

        /// <summary>
        /// Метод получения списка всех файлов из базы данных
        /// </summary>
        /// <returns>Список файлов в базе данных</returns>
        public async Task<List<FileDto>> GetAllFilesAsync(SortAndFilterFilesDto dto)
        {
            return await _fileRepository.GetFilesAsync(dto);
        }
        
        /// <summary>
        /// Метод получения информации о файле по id
        /// </summary>
        /// <param name="id">id файла в БД</param>
        /// <returns><see cref="FileDto"/></returns>
        public async Task<FileDto> GetFileInfoById(Guid id)
        {
            var file = await _fileRepository.GetAsync(id);
            return file!.ToFileDto();
        }

        /// <summary>
        /// Сохраняет поток в файл
        /// </summary>
        /// <param name="fileStream">Поток</param>
        /// <param name="fileName">Имя файла</param>
        private async Task CopyFileAsync(Stream fileStream, string fileName)
        {   
            var filePath = Path.Combine(_storageOptions.FileDir!, fileName);

            await using (fileStream)
            {
                await using (var dest = new FileStream(filePath, FileMode.Create))
                {
                    await fileStream.CopyToAsync(dest);
                }
            }
        }


        /// <summary>
        /// Создает новую запись в БД
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns></returns>
        private async Task<FileEntity> AddToDbAsync(string fileName)
        {
            FileEntity entity = new()
            {
                Name = fileName
            };

            await _fileRepository.CreateAsync(entity);
            return entity;
        }
        #endregion
    }
}

