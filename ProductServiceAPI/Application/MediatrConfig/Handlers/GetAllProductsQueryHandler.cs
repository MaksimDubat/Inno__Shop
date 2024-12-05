using MediatR;
using ProductServiceAPI.Application.MediatrConfig.Queries;
using ProductServiceAPI.Domain.Entities;
using ProductServiceAPI.Interface;

namespace ProductServiceAPI.Application.MediatrConfig.Hendlers
{
    /// <summary>
    /// Обработчик запроса получения всех продуктов.
    /// </summary>
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _repository;
        public GetAllProductsQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync(cancellationToken);
            return products;
        }
    }
}
