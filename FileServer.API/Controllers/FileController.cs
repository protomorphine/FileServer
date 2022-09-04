using Microsoft.AspNetCore.Mvc;
using FileServer.Core.Services.Interfaces;

namespace FileServer.API.Controllers
{
    [ApiController]
    [Route("api/file")]
    public class FileController : ControllerBase
    {
        #region поля

        private readonly IFileService _fileService;

        #endregion

        #region конструктор

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
        /// <response code="200">Возвращает id загруженного файла</response>
        [HttpPost("upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var result = await _fileService.Upload(file.OpenReadStream(), file.FileName);
            return Ok(result);
        }

        /// <summary>
        /// Метод выгрузки файла с сервера
        /// </summary>
        /// <param name="id">id файла в БД</param>
        [HttpGet("download/{id}")]
        public async Task<FileContentResult> DownloadFile(Guid id)
        {
            var result =  await _fileService.Download(id);
            return new FileContentResult(result.FileContent, result.ContentType)
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

        #endregion
    }
}