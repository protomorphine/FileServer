using FileServer.Core.Services.Interfaces;
using FileServer.Core.Repositories;
using FileServer.Core.Extensions;
using FileServer.Core.Models;

namespace FileServer.Core.Services
{
    public class FileService : IFileService
    {
        private readonly StorageOptions _storageOptions;
        private readonly IFileRepository _fileRepository;

        public FileService(StorageOptions storageOptions, IFileRepository fileRepository)
        {
            _storageOptions = storageOptions;
            _fileRepository = fileRepository;
        }

        /// <summary>
        /// Загрузка файла
        /// </summary>
        /// <param name="file"> Stream файла </param>
        /// <param name="fileName"> Имя Файла </param>
        /// <returns> id файла в БД </returns>
        public async Task<Guid> Upload(Stream file, string fileName)
        {
            using var dbTransaction = await _fileRepository.Context.Database.BeginTransactionAsync();
            try
            {
                Task<FileEntity> fileTask = AddToDbAsync(fileName);
                Task copyFileTask = CopyFileAsync(file, fileName);
                FileEntity? fileEntity = null;
                var getFileEntityTask = Task.Run(async () => fileEntity = await fileTask);

                await Task.WhenAll(copyFileTask, getFileEntityTask);
                
                await dbTransaction.CommitAsync();

                return fileEntity!.Id;
            }
            catch (Exception ex)
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

            var filePath = Path.Combine(_storageOptions.FileDir, file.Name);

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
            await _fileRepository.DeleteAsync(file);

            var filePath = Path.Combine(_storageOptions.FileDir, file.Name);

            if (!File.Exists(filePath))
                throw new FileNotFoundException("Файл не найден!");

            await Task.Run(() => File.Delete(filePath));
        }

        private async Task CopyFileAsync(Stream fileStream, string fileName)
        {
            var filePath = Path.Combine(_storageOptions.FileDir, fileName);

            using (fileStream)
            {
                using (var dest = new FileStream(filePath, FileMode.Create))
                {
                    await fileStream.CopyToAsync(dest);
                }
            }
        }

        private async Task<FileEntity> AddToDbAsync(string fileName)
        {
            FileEntity entity = new()
            {
                Name = fileName
            };

            await _fileRepository.CreateAsync(entity);
            return entity;
        }
    }
}

