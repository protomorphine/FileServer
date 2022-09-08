namespace FileServer.Core.Models
{
    /// <summary>
    /// Конфигурация файлового хранилища
    /// </summary>
    public class StorageOptions
    {
        /// <summary>
        /// Папка на диске, куда складываются файлы
        /// </summary>
        public string? FileDir { get; set; }
    }
}
