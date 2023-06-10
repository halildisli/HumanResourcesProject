using HumanResourcesProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesProject.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : class
    {
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> models);
        bool Remove(T model);
        bool RemoveRange(List<T> models);
        bool Update(T model);

        Task<int> SaveAsync();
    }
}
