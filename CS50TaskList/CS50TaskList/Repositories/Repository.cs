using CS50TaskList.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CS50TaskList.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        public Repository(ApplicationDbContext context, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _userManager = userManager;
            _contextAccessor = httpContextAccessor;
        }

        public async Task Create(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
            }
            catch (Exception ex) { throw; }
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null)
        {
            try
            {
                IQueryable<T> query = _dbSet.AsNoTracking();

                if (typeof(T).GetProperty("UserId") is not null)
                {
                    var userId = _userManager.GetUserId(_contextAccessor.HttpContext.User);

                    query = query.Where(x => EF.Property<string>(x, "UserId") == userId);
                }

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                var entities = await query.ToListAsync();
                return entities;
            }
            catch (ArgumentNullException ex) { throw; }
            catch (OperationCanceledException ex) { throw; }
            catch (Exception ex) { throw; }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                IQueryable<T> query = _dbSet;

                if (typeof(T).GetProperty("UserId") is not null)
                {
                    var userId = _userManager.GetUserId(_contextAccessor.HttpContext.User);

                    query = query.Where(x => EF.Property<string>(x, "UserId") == userId);
                }

                var entity = await query.FirstOrDefaultAsync(x => EF.Property<int>(x, "Id") == id);

                return entity;
            }
            catch (ArgumentNullException ex) { throw; }
            catch (OperationCanceledException ex) { throw; }
            catch (Exception ex) { throw; }
        }

        public void Remove(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
            }
            catch (Exception ex) { throw; }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) { throw; }
        }

        public void Update(T entity)
        {
            try
            {
                _dbSet.Update(entity);
            }
            catch (Exception ex) { throw; }
        }
    }
}
