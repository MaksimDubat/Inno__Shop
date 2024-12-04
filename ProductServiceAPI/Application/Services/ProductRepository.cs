using Microsoft.EntityFrameworkCore;
using ProductServiceAPI.DataBase;
using ProductServiceAPI.Domain.Entities;
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
        /// <summary>
        /// Получение наименование товара.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellation"></param>
        public async Task<Product> GetByNameAsync(string name, CancellationToken cancellation)
        {
            return await _context.Products.FirstOrDefaultAsync(u => u.Name == name, cancellation); 
        }
    }
}
