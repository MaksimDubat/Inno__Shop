using Microsoft.EntityFrameworkCore;
using UserServiceAPI.DataBaseAccess;
using UserServiceAPI.Interface;

namespace UserServiceAPI.Infrastructure.Common
{
    /// <summary>
    /// Базовый репозиторий по работе с БД.
    /// </summary>
    public class EfRepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly MutableInnoShopDbContext MutableDbContext;
        

        /// <inheritdoc/>
        public EfRepositoryBase(MutableInnoShopDbContext mutableDbContext)
        {
            MutableDbContext = mutableDbContext;
        }

        /// <inheritdoc/>        
        public async Task AddAsync(T entity, CancellationToken cancellation)
        {
            await MutableDbContext.AddAsync(entity, cancellation);
            await MutableDbContext.SaveChangesAsync(cancellation);
        }
        /// <inheritdoc/>  
        public async Task<T> DeleteAsync(int id, CancellationToken cancellation)
        {
           var entity = await MutableDbContext.Set<T>().FindAsync(id, cancellation);
           if (entity == null)
            {
                throw new KeyNotFoundException();
            }
           MutableDbContext.Remove(entity);
           await MutableDbContext.SaveChangesAsync(cancellation);
           return entity;
        }
        /// <inheritdoc/>  
        public Task<List<T>> GetAllAsync(CancellationToken cancellation)
        {
            return MutableDbContext.Set<T>().ToListAsync(cancellation);
        }
        /// <inheritdoc/>  
        public async Task<T> GetAsync(int id, CancellationToken cancellation)
        {
            return await MutableDbContext.Set<T>().FindAsync(id, cancellation);
        }
        /// <inheritdoc/>  
        public Task UpdateAsync(T entity, CancellationToken cancellation)
        {
            MutableDbContext.Update(entity);
            return MutableDbContext.SaveChangesAsync(cancellation);
        }
    }
}
