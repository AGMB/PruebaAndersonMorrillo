using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BancoPichincha.Core.Interfaces.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task SaveChangesAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        IQueryable<T> GetAllIQuerable();
        Task<T> GetByIdAsync(object id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
