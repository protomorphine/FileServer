using FileServer.Core.Models;

namespace FileServer.API.Models
{
    /// <summary>
    /// Конфигурация приложения
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Настройки файлового хранилища
        /// </summary>
        public StorageOptions? StorageOptions { get; set; }

        /// <summary>
        /// Настройки подключения к бд
        /// </summary>
        public DbOptions? DbOptions { get; set; }
    }
}
