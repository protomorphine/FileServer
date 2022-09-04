namespace FileServer.Core.Models
{
    public class FileEntity
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public string Name { get; set; } = String.Empty;
    }
}
