using MediatR;
using ProductServiceAPI.Application.MediatrConfig.Queries;
using ProductServiceAPI.Domain.Entities;
using ProductServiceAPI.Interface;

namespace ProductServiceAPI.Application.MediatrConfig.Hendlers
{
    /// <summary>
    /// ОБработчик запроса на получение отфильтрованных продуктов.
    /// </summary>
    public class GetFilteredProductQueryHandler : IRequestHandler<GetFilteredProductQuery, IEnumerable<Product>>
    {
        private readonly IProductFiltration _filtartion;
        public GetFilteredProductQueryHandler(IProductFiltration filtartion)
        {
            _filtartion = filtartion;
        }
        public async Task<IEnumerable<Product>> Handle(GetFilteredProductQuery request, CancellationToken cancellationToken)
        {
            var filteredProducts = await _filtartion.GetFilteredProductsAsync(request.Filters, cancellationToken);
            return filteredProducts;
        }
    }
}
