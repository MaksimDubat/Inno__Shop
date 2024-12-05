using MediatR;
using ProductServiceAPI.Application.MediatrConfig.Queries;
using ProductServiceAPI.Domain.Entities;
using ProductServiceAPI.Interface;

namespace ProductServiceAPI.Application.MediatrConfig.Hendlers
{
    /// <summary>
    /// Обработчик запроса на получение продукта по наименованию.
    /// </summary>
    public class GetProductNameQueryHandler : IRequestHandler<GetProductNameQuery, Product>
    {
        private readonly IProductRepository _repository;

        public GetProductNameQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product> Handle(GetProductNameQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByNameAsync(request.Name, cancellationToken);
            if (product == null)
            {
                throw new KeyNotFoundException();
            }
            return product;
        }
    }
}
