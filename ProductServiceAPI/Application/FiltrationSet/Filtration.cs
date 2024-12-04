using Microsoft.EntityFrameworkCore;
using ProductServiceAPI.DataBase;
using ProductServiceAPI.Domain.Entities;
using ProductServiceAPI.Interface;
using ProductServiceAPI.Model.Filters;
using System.Collections;
using System.Linq.Expressions;

namespace ProductServiceAPI.FiltrationSet
{
    /// <summary>
    /// Класс для рализиации фильтрации продукта.
    /// </summary>
    public class Filtration : IProductFiltration
    {
        private readonly MutableInnoShopProductDbContext _dbContext;

        public Filtration(MutableInnoShopProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Product>> GetFilteredProductsAsync(ProductFiltersModel filter, CancellationToken cancellationToken)
        {
            var query = _dbContext.Products.AsNoTracking().AsQueryable();   

            if(!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(p => p.Name.Contains(filter.Name));
            }
            if (filter.MinPrice.HasValue)
            {
                query= query.Where(p => p.Price >= filter.MinPrice.Value);
            }
            if(filter.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);
            }
            if(filter.IsActive.HasValue)
            {
                query = query.Where(p => p.Equals(filter.IsActive.Value));
            }
            if(filter.CreatedBefore.HasValue)
            {
                query = query.Where(p => p.CreatedAt <= filter.CreatedBefore.Value);
            }
            if(filter.CreatedAfter.HasValue)
            {
                query = query.Where(p => p.CreatedAt >= filter.CreatedAfter.Value);
            }
            cancellationToken.ThrowIfCancellationRequested();
            return await query.ToListAsync(cancellationToken);   
        }
    }
}
