using HumanResourcesProject.Application.Repositories;
using HumanResourcesProject.Domain.Entities;
using HumanResourcesProject.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Persistance.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : class
    {
        private readonly HumanResourcesProjectDbContext _context;

        public WriteRepository(HumanResourcesProjectDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T model)
        {
            EntityEntry<T> entityEntry=await Table.AddAsync(model);
            return entityEntry.State==EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> model)
        {
            await Table.AddRangeAsync(model);
            return true;
        }

        public bool Remove(T model)
        {
            EntityEntry entityEntry = Table.Remove(model);
            return entityEntry.State == EntityState.Deleted;
        }
        public bool RemoveRange(List<T> models)
        {
            Table.RemoveRange(models);
            return true;
        }
        public bool Update(T model)
        {
            EntityEntry entityEntry=Table.Update(model);
            return entityEntry.State == EntityState.Modified;
        }
        public async Task<int> SaveAsync()
            =>await _context.SaveChangesAsync();
        
    }
}
