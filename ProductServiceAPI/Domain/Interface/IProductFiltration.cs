using Microsoft.EntityFrameworkCore;
using ProductServiceAPI.DataBase;
using ProductServiceAPI.Domain.Entities;
using ProductServiceAPI.Model.Filters;

namespace ProductServiceAPI.Interface
{
    /// <summary>
    /// Интерфейс для фильтрации продкутов.
    /// </summary>
    public interface IProductFiltration
    {
        /// <summary>
        /// Фильтрация продуктов.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        Task<IEnumerable<Product>> GetFilteredProductsAsync(ProductFiltersModel filter, CancellationToken cancellationToken);
    }
}
