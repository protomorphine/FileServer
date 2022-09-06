using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core;
using FileServer.Core.Models;
using FileServer.Core.Repositories;
using FileServer.Infrastructure.Data;

namespace FileServer.Infrastructure.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly AppDbContext _dbContext;

        public FileRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<FileEntity> CreateAsync(FileEntity fileEntity)
        {
            await _dbContext.Files.AddAsync(fileEntity);
            await _dbContext.SaveChangesAsync();
            return fileEntity;
        }

        public async Task<FileEntity> GetAsync(Guid id)
        {
            return await _dbContext.Files.FirstOrDefaultAsync(it => it.Id == id); ;
        }

        public async Task DeleteAsync(FileEntity fileEntity)
        {
            _dbContext.Files.Remove(fileEntity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
