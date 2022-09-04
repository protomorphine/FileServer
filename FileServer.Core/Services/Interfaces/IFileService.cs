using FileServer.Core.Models;

namespace FileServer.Core.Services.Interfaces
{
    public interface IFileService
    {
        Task<Guid> Upload(Stream file, string fileName);

        Task<FileResponceModel> Download(Guid id);

        Task Delete(Guid id);
    }
}
