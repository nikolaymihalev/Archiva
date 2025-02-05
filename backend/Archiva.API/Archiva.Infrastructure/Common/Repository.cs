using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archiva.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Archiva.Infrastructure.Common
{
    public class Repository : IRepository
    {
        private DbContext context;
        
        private DbSet<T> DbSet<T>() where T : class => this.context.Set<T>();

        public Repository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task AddAsync<T>(T entity) where T : class => await this.DbSet<T>().AddAsync(entity);

        public IQueryable<T> All<T>() where T : class => this.DbSet<T>().AsQueryable();

        public IQueryable<T> AllReadonly<T>() where T : class => this.DbSet<T>().AsQueryable().AsNoTracking();

        public async Task DeleteAsync<T>(object id) where T : class
        {
            T? entity = await GetByIdAsync<T>(id);

            if (entity != null)
            {
                this.DbSet<T>().Remove(entity);
            }
        }

        public async Task<T?> GetByIdAsync<T>(object id) where T : class => await this.DbSet<T>().FindAsync(id);

        public Task<int> SaveChangesAsync() => this.context.SaveChangesAsync();

        public void Delete<T>(T entity) where T : class
        {
            EntityEntry entry = this.context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.DbSet<T>().Attach(entity);
            }

            entry.State = EntityState.Deleted;
        }
    }
}