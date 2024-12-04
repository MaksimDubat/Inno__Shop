using ProductServiceAPI.Domain.Entities;

namespace ProductServiceAPI.Interface
{
    /// <summary>
    /// Интерфейс для работы с продуктами, расширяющий базовый.
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetByNameAsync(string name, CancellationToken token);
    }
}
