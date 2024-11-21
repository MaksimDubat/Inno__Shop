using Microsoft.EntityFrameworkCore;
using ProductServiceAPI.DataBase;
using ProductServiceAPI.Entities;
using ProductServiceAPI.Model.Filters;

namespace ProductServiceAPI.Interface
{
    public interface IProductFiltration
    {
        Task<IEnumerable<Product>> GetFilteredProductsAsync(ProductFiltersModel filter, CancellationToken cancellationToken);
    }
}
