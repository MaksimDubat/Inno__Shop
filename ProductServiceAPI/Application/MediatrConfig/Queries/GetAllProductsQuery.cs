using MediatR;
using ProductServiceAPI.Domain.Entities;

namespace ProductServiceAPI.Application.MediatrConfig.Queries
{
    /// <summary>
    /// Модель запроса на получение продуктов.
    /// </summary>
    public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
