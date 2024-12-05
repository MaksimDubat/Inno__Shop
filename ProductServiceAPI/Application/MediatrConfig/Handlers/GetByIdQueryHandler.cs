using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using ProductServiceAPI.Application.MediatrConfig.Queries;
using ProductServiceAPI.Domain.Entities;
using ProductServiceAPI.Interface;

namespace ProductServiceAPI.Application.MediatrConfig.Hendlers
{
    /// <summary>
    /// Обработчик запроса для получения продукта по идентификатору.
    /// </summary>
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Product>
    {
        private readonly IProductRepository _repository;
        public GetByIdQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }
        public async Task<Product> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetAsync(request.Id, cancellationToken);
            if (product == null)
            {
                throw new KeyNotFoundException();
            }
            return product;
        }
    }
}
