using MediatR;
using ProductServiceAPI.Domain.Entities;

namespace ProductServiceAPI.Application.MediatrConfig.Queries
{
    /// <summary>
    /// Модель запроса продукта по наименованию.
    /// </summary>
    public class GetProductNameQuery : IRequest<Product>
    {
        /// <summary>
        /// Наименование продукта.
        /// </summary>
        public string Name { get; set; }

        public GetProductNameQuery(string name)
        {
            Name = name;
        }
    }
}
