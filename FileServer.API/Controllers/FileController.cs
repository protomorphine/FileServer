using FileServer.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using FileServer.Core.Services.Interfaces;

namespace FileServer.API.Controllers
{
    [ApiController]
    [Route("api/file")]
    public class FileController : ControllerBase
    {
        #region поля

        /// <summary>
        /// Сервис для работы с файлами
        /// </summary>
        private readonly IFileService _fileService;

        #endregion

        #region конструктор

        /// <summary>
        /// Создает новый экземпляр <see cref="FileController"/>
        /// </summary>
        /// <param name="fileService">сервис для работы с файлами</param>
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        #endregion

        #region методы
        /// <summary>
        /// Загрузка файла в папку, указанную в appsettings.json
        /// </summary>
        /// <param name="file">Файл</param>
        /// <returns>id файла</returns>
        [HttpPost("upload")]
        public async Task<Guid> UploadFile(IFormFile file)
        {
            return await _fileService.Upload(file.OpenReadStream(), file.FileName);
        }

        /// <summary>
        /// Метод выгрузки файла с сервера
        /// </summary>
        /// <param name="id">id файла в БД</param>
        [HttpGet("download/{id}")]
        public async Task<FileContentResult> DownloadFile(Guid id)
        {
            var result =  await _fileService.Download(id);
            return new FileContentResult(result.FileContent!, result.ContentType!)
            {
                FileDownloadName = result.Name
            };
        }

        /// <summary>
        /// Удаляет файл с сервера
        /// </summary>
        /// <param name="id">id файла в БД</param>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteFile(Guid id)
        {
            await _fileService.Delete(id);
            return Ok();
        }
        
        /// <summary>
        /// Получение информации о файле
        /// </summary>
        /// <param name="id">id файла в БД</param>
        /// <returns><see cref="FileDto"/></returns>
        [HttpGet("{id}")]
        public async Task<FileDto> GetFileInfo(Guid id)
        {
            return await _fileService.GetFileInfoById(id);
        }

        /// <summary>
        /// Получает список всех файлов
        /// </summary>
        /// <returns>список файлов</returns>
        [HttpGet("all")]
        public async Task<List<FileDto>> GetAllFiles([FromQuery] SortAndFilterFilesDto dto)
        {
            return await _fileService.GetAllFilesAsync(dto);
        }

        #endregion
    }
}