using Microsoft.EntityFrameworkCore;
using ProductServiceAPI.DataBase;
using ProductServiceAPI.Entities;
using ProductServiceAPI.Interface;
using ProductServiceAPI.Model.Filters;
using System.Collections;
using System.Linq.Expressions;

namespace ProductServiceAPI.FiltrationSet
{
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
            else if (filter.MinPrice.HasValue)
            {
                query= query.Where(p => p.Price <= filter.MinPrice.Value);
            }
            else if(filter.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price >= filter.MaxPrice.Value);
            }
            else if(filter.IsActive.HasValue)
            {
                query = query.Where(p => p.Equals(filter.IsActive.Value));
            }
            else if(filter.CreatedBefore.HasValue)
            {
                query = query.Where(p => p.CreatedAt <= filter.CreatedBefore.Value);
            }
            else if(filter.CreatedAfter.HasValue)
            {
                query = query.Where(p => p.CreatedAt >= filter.CreatedAfter.Value);
            }
            
            return await query.ToListAsync();   
        }
    }
}
