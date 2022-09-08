namespace FileServer.API.Models
{
    /// <summary>
    /// Настройки подключения к бд
    /// </summary>
    public class DbOptions
    {
        /// <summary>
        /// Строка подключения
        /// </summary>
        public string? ConnectionString { get; set; }
    }
}
