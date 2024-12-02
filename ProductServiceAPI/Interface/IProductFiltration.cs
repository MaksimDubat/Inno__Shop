using Microsoft.EntityFrameworkCore;
using ProductServiceAPI.DataBase;
using ProductServiceAPI.Entities;
using ProductServiceAPI.Model.Filters;

namespace ProductServiceAPI.Interface
{
    /// <summary>
    /// Интерфейс для фильтрации продкутов.
    /// </summary>
    public interface IProductFiltration
    {
        Task<IEnumerable<Product>> GetFilteredProductsAsync(ProductFiltersModel filter, CancellationToken cancellationToken);
    }
}
