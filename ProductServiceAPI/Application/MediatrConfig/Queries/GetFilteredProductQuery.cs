using MediatR;
using ProductServiceAPI.Domain.Entities;
using ProductServiceAPI.Model.Filters;

namespace ProductServiceAPI.Application.MediatrConfig.Queries
{
    /// <summary>
    /// Модель запроса на получение отфильтрованых товаров.
    /// </summary>
    public class GetFilteredProductQuery : IRequest<IEnumerable<Product>>
    {
        /// <summary>
        /// Фильтр продуктов.
        /// </summary>
        public ProductFiltersModel Filters { get; set; }
        public GetFilteredProductQuery(ProductFiltersModel filters)
        {
            Filters = filters;
        }
    }
}
