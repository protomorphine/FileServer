namespace FileServer.Core.Models
{
    /// <summary>
    /// Модель ответа при получении файла при использовании http
    /// </summary>
    public class FileResponceModel
    {
        /// <summary>
        /// Имя файла
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Http заголовок - тип содержимого
        /// </summary>
        public string? ContentType { get; set; }

        /// <summary>
        /// Содержимое файла, представленное массивом байт
        /// </summary>
        public byte[]? FileContent { get; set; }
    }
}
