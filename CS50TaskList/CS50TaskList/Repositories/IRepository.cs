using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CS50TaskList.Repositories
{
    public interface IRepository<T> where T : class
    {
        public Task Create(T entity);

        public Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);

        public Task<T> GetByIdAsync(int id);

        public void Remove(T entity);
        public Task SaveChangesAsync();
        public void Update(T entity);
    }
}
