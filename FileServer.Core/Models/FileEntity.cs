using FileServer.Core.Dtos;

namespace FileServer.Core.Models
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
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// Мапинг сущности на ДТО
        /// </summary>
        /// <returns>ДТО - файл</returns>
        public FileDto ToFileDto() => 
            new FileDto
            {
                FileId = Id,
                FileName = Name
            };
            
    }
}
