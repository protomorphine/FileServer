using FileServer.Core.Dtos;

namespace FileServer.Core.Entities
{
    /// <summary>
    /// Сущность - файл
    /// </summary>
    public class FileEntity
    {
        /// <summary>
        /// Уникальный идентификатор файла
        /// </summary>
        public Guid Id { get; init; } = Guid.NewGuid();

        /// <summary>
        /// Имя файла
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Дата добавления файла в БД
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Мапинг сущности на ДТО
        /// </summary>
        /// <returns>ДТО - файл</returns>
        public FileDto ToFileDto() =>
            new FileDto
            {
                FileId = Id,
                FileName = Name,
                CreatedAt = CreatedAt
            };

    }
}
