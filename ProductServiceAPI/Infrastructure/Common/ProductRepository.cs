using Microsoft.EntityFrameworkCore;
using ProductServiceAPI.DataBase;
using ProductServiceAPI.Entities;
using ProductServiceAPI.Interface;

namespace ProductServiceAPI.Infrastructure.Common
{
    /// <summary>
    /// Репозиторий по работе с продуктами, расширяющий базовый.
    /// </summary>
    public class ProductRepository : EfRepositoryBase<Product>, IProductRepository
    {
        private readonly MutableInnoShopProductDbContext _context;
        public ProductRepository(MutableInnoShopProductDbContext mutableDbContext) : base(mutableDbContext)
        {
            _context = mutableDbContext;
        }

        public async Task<Product> GetByNameAsync(string name, CancellationToken cancellation)
        {
            return await _context.Products.FirstOrDefaultAsync(u => u.Name == name, cancellation); 
        }
    }
}
