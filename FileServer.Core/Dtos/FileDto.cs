namespace FileServer.Core.Dtos
{
    public class FileDto
    {
        /// <summary>
        /// Имя файла
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// Id файла
        /// </summary>
        public Guid FileId { get; set; }
        
        /// <summary>
        /// Дата добавления файла в БД
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
