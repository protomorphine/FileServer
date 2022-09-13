using Microsoft.EntityFrameworkCore;
using FileServer.Core.Models;
using FileServer.Core.Repositories;
using FileServer.Infrastructure.Data;
using FileServer.Core.Dtos;
using FileServer.Core.Extensions;

namespace FileServer.Infrastructure.Repositories
{
    /// <summary>
    /// Репозиторий для работы с файлами
    /// </summary>
    public class FileRepository : IFileRepository
    {
        #region поля

        /// <summary>
        /// Контекст бд
        /// </summary>
        private readonly DbContext _dbContext;
        
        /// <summary>
        /// Коллекция сущностей - файл
        /// </summary>
        private readonly DbSet<FileEntity> _files;

        #endregion

        #region конструкторы

        /// <summary>
        /// Создает новый экземпляр <see cref="FileRepository"/>
        /// </summary>
        /// <param name="dbContext">Контекст бд</param>
        public FileRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _files = dbContext.Files;
        }

        #endregion

        #region методы

        /// <summary>
        /// Метод создания сущности в бд
        /// </summary>
        /// <param name="fileEntity">сущность файла</param>
        /// <returns>созданная в бд сущность</returns>
        public async Task<FileEntity> CreateAsync(FileEntity fileEntity)
        {
            await _files.AddAsync(fileEntity);
            await _dbContext.SaveChangesAsync();
            return fileEntity;
        }

        /// <summary>
        /// Метод получения сущности по id
        /// </summary>
        /// <param name="id">id сущности в формате Guid</param>
        /// <returns>сущность файла</returns>
        public async Task<FileEntity> GetAsync(Guid id)
        {
            return await _files.FirstOrDefaultAsync(it => it.Id == id); ;
        }

        /// <summary>
        /// Удаляет сущность из бд
        /// </summary>
        /// <param name="fileEntity">сущность файла</param>
        public async Task DeleteAsync(FileEntity fileEntity)
        {
            _files.Remove(fileEntity);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает список всех файлов в БД
        /// </summary>
        /// <returns>Список всех файлов</returns>
        public async Task<List<FileDto>> GetAllAsync()
        {
            return (await _files.ToListAsync()).ToFileDtoList();
        }

        #endregion

    }
}
