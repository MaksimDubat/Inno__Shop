using ProductServiceAPI.Entities;

namespace ProductServiceAPI.Interface
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetByNameAsync(string name, CancellationToken token);
    }
}
