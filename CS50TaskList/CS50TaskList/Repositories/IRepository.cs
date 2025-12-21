using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CS50TaskList.Repositories
{
    /// <summary>
    ///     Interface for a repository class with database context and methods used to CRUD entities by the type of <see cref="T"/>.
    /// </summary>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        ///     Queries the db context to add a new tracked <see cref="T"/> entity to later be saved by <see cref="SaveChangesAsync()"/>.
        /// </summary>
        /// <param name="entity">Entity representing the data.</param>
        public Task Create(T entity);

        /// <summary>
        ///     Queries the db context to return a <see cref="List{T}"/>.
        /// </summary>
        /// <param name="predicate">Lambda expression to select items to be returned.</param>
        public Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);

        /// <summary>
        ///     Queries the db context to return a <see cref="T"/>.
        /// </summary>
        /// <param name="id">Id of <see cref="T"/></param>
        public Task<T> GetByIdAsync(int id);

        /// <summary>
        ///     Queries the db context to remove a <see cref="T"/>.
        /// </summary>
        /// <param name="entity">Entity representing the data.</param>
        public void Remove(T entity);

        /// <summary>
        ///     Queries the db context to save the changes made to the db.
        /// </summary>
        public Task SaveChangesAsync();

        /// <summary>
        ///     Queries the db context to update a <see cref="T"/>.
        /// </summary>
        /// <param name="entity">Entity representing the data.</param>
        public void Update(T entity);
    }
}
