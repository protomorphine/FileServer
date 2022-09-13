using FileServer.Core.Dtos;
using FileServer.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FileServer.Core.Repositories
{
    /// <summary>
    /// Репозиторий для работы с файлами
    /// </summary>
    public interface IFileRepository
    {
        /// <summary>
        /// Метод создания сущность в бд
        /// </summary>
        /// <param name="fileEntity">файл</param>
        /// <returns></returns>
        Task<FileEntity> CreateAsync(FileEntity fileEntity);


        /// <summary>
        /// Метод получения сущности из бд
        /// </summary>
        /// <param name="id">id файла</param>
        /// <returns></returns>
        Task<FileEntity> GetAsync(Guid id);


        /// <summary>
        /// Метод удаления сущности из бд
        /// </summary>
        /// <param name="fileEntity">сущность</param>
        /// <returns></returns>
        Task DeleteAsync(FileEntity fileEntity);

        /// <summary>
        /// Получение всех записей в бд
        /// </summary>
        /// <returns>Список файлов в бд</returns>
        Task<List<FileDto>> GetAllAsync();
    }
}
