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
    }
}
