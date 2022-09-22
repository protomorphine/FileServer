using FileServer.Core.Dtos;
using FileServer.Core.Models;

namespace FileServer.Core.Services.Interfaces
{
    /// <summary>
    /// Сервис для работы с файлами
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Метод загрузки файла на сервер
        /// </summary>
        /// <param name="file">поток файла</param>
        /// <param name="fileName">имя файла</param>
        /// <returns>Уникальный идентификатор файла в бд</returns>
        Task<Guid> Upload(Stream file, string fileName);

        /// <summary>
        /// Метод загрузки файла с сервера
        /// </summary>
        /// <param name="id">id файла в формате Guid</param>
        /// <returns>Модель ответа <see cref="FileResponceModel"/></returns>
        Task<FileResponceModel> Download(Guid id);

        /// <summary>
        /// Метод удаления файла из хранилища и бд
        /// </summary>
        /// <param name="id">id файла в формате Guid</param>
        /// <returns></returns>
        Task Delete(Guid id);

        /// <summary>
        /// Метод получаения всех сущностей файлов из бд
        /// </summary>
        /// <returns></returns>
        Task<List<FileDto>> GetAllFilesAsync(SortAndFilterFilesDto dto);
    }
}
