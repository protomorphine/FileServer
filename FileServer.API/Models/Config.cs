using FileServer.Core.Models;

namespace FileServer.API.Models
{
    public class Config
    {
        public StorageOptions StorageOptions { get; set; }

        public DbOptions DbOptions { get; set; }
    }
}
