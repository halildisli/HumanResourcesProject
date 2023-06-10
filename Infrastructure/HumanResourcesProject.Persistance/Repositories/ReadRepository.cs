using HumanResourcesProject.Application.Repositories;
using HumanResourcesProject.Domain.Entities;
using HumanResourcesProject.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Persistance.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : class
    {
        private readonly HumanResourcesProjectDbContext _context;

        public ReadRepository(HumanResourcesProjectDbContext context)
        {
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll()
            => Table;

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method)
            => Table.Where(method);

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method)
            => await Table.FirstOrDefaultAsync(method);
        public async Task<T> GetByIdAsync(string id)
            => await Table.FindAsync(Guid.Parse(id));

        public async Task<T> GetByIdAspAsync(string id)
            => await Table.FindAsync(id);

		
	}
}
