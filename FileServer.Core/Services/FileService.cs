using FileServer.Core.Services.Interfaces;
using FileServer.Core.Repositories;
using FileServer.Core.Models;

namespace FileServer.Core.Services
{
    public class FileService : IFileService
    {
        private readonly Config _config;
        private readonly IFileRepository _fileRepository;

        public FileService(Config config, IFileRepository fileRepository)
        {
            _config = config;
            _fileRepository = fileRepository;
        }

        /// <summary>
        /// Загружает файл и складывает в папку, указанную в appsettings.json
        /// </summary>
        /// <param name="file">Полученный IFormFile file</param>
        public async Task<Guid> Upload(Stream file, string fileName)
        {
            var filePath = Path.Combine(_config.FileDir, fileName);

            using (file)
            {
                using (var dest = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(dest);
                }
            }

            FileEntity fileEntity = new()
            {
                Name = fileName
            };

            await _fileRepository.CreateAsync(fileEntity);
            
            return fileEntity.Id;
        }

        /// <summary>
        /// Получает файл из папки, указанной в appsettings.json
        /// </summary>
        /// <param name="id">id файла в формате Guid</param>
        public async Task<FileResponceModel> Download(Guid id)
        {
            var file = await _fileRepository.GetAsync(id);

            var filePath = Path.Combine(_config.FileDir, file.Name);

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
            var entity = await _fileRepository.GetAsync(id);
            await _fileRepository.DeleteAsync(entity);

            var filePath = Path.Combine(_config.FileDir, entity.Name);

            if (!File.Exists(filePath))
                throw new FileNotFoundException("Файл не найден!");

            await Task.Run(() => File.Delete(filePath));
        }
    }
}

