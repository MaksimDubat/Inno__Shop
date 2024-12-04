using Microsoft.EntityFrameworkCore;
using ProductServiceAPI.DataBase;
using ProductServiceAPI.Domain.Entities;
using ProductServiceAPI.Interface;

namespace ProductServiceAPI.Infrastructure.Common
{
    /// <summary>
    /// Базовый репозиторий по работе с БД.
    /// </summary>
    public class EfRepositoryBase<T> : IRepository<T> where T : Product
    {
        private readonly MutableInnoShopProductDbContext MutableDbContext;
        private readonly IHttpContextAccessor HttpContextAccessor;
        /// <inheritdoc/>
        public EfRepositoryBase(MutableInnoShopProductDbContext mutableDbContext)
        {
            MutableDbContext = mutableDbContext;
            HttpContextAccessor = new HttpContextAccessor();
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
            return MutableDbContext.Set<T>()
                .Where(p => p.IsActive)
                .ToListAsync(cancellation);
        }
        /// <inheritdoc/>  
        public async Task<List<T>> GetAsync(int id, CancellationToken cancellation)
        {
            return await MutableDbContext.Set<T>()
                .Where(p=> p.Id == id && p.IsActive)
                .ToListAsync(cancellation);
        }
        /// <inheritdoc/>  
        public Task UpdateAsync(T entity, CancellationToken cancellation)
        {
            MutableDbContext.Update(entity);
            return MutableDbContext.SaveChangesAsync(cancellation);
        }
    }
}
