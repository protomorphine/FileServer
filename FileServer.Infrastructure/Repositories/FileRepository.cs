using Microsoft.EntityFrameworkCore;
using FileServer.Core.Models;
using FileServer.Core.Repositories;
using FileServer.Infrastructure.Data;

namespace FileServer.Infrastructure.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<FileEntity> _files;

        public FileRepository(AppDbContext dbContext)
        {
            _dbContext = Context = dbContext;
            _files = dbContext.Files;
        }

        public DbContext Context { get; }

        //DbContext IFileRepository.Context => throw new NotImplementedException();

        public async Task<FileEntity> CreateAsync(FileEntity fileEntity)
        {
            await _files.AddAsync(fileEntity);
            await _dbContext.SaveChangesAsync();
            return fileEntity;
        }

        public async Task<FileEntity> GetAsync(Guid id)
        {
            return await _files.FirstOrDefaultAsync(it => it.Id == id); ;
        }

        public async Task DeleteAsync(FileEntity fileEntity)
        {
            _files.Remove(fileEntity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
