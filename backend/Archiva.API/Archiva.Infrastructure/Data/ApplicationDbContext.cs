using Archiva.Infrastructure.Data.Configurations;
using Archiva.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Archiva.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<UserDocument> UserDocuments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserDocumentConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
