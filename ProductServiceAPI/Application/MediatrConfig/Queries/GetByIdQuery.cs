using MediatR;
using ProductServiceAPI.Domain.Entities;

namespace ProductServiceAPI.Application.MediatrConfig.Queries
{
    /// <summary>
    /// Модель запроса на получение продукта по идентификатору.
    /// </summary>
    public class GetByIdQuery : IRequest<Product>
    {
        /// <summary>
        /// Идентификатор продукта.
        /// </summary>
        public int Id { get; set; }
    }
}
