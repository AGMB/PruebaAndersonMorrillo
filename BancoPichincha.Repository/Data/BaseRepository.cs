using BancoPichincha.Core.Interfaces.Repository;
using BancoPichincha.Repository.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BancoPichincha.Repository.Data
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly TestBancoPichinchaContext _dbContext;

        public BaseRepository(TestBancoPichinchaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T entity)
        {
           _dbContext.Set<T>().Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public IQueryable<T> GetAllIQuerable()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public void Update(T entity)
        {
             _dbContext.Update(entity);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public void Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
