using Microsoft.EntityFrameworkCore;
using FileServer.Core.Models;

namespace FileServer.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<FileEntity> Files { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileEntity>().ToTable("files");
        }
    }
}
