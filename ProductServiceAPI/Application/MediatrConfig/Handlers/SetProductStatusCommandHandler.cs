using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductServiceAPI.Application.MediatrConfig.Commands;
using ProductServiceAPI.DataBase;
using ProductServiceAPI.Domain.Entities;
using ProductServiceAPI.Interface;

namespace ProductServiceAPI.Application.MediatrConfig.Hendlers
{
    /// <summary>
    /// Обработчик команды изменения статуса товара.
    /// </summary>
    public class SetProductStatusCommandHandler : IRequestHandler<SetProductStatusCommand, bool>
    {
        private readonly IProductRepository _repository;
        private readonly MutableInnoShopProductDbContext _context;
        public SetProductStatusCommandHandler(IProductRepository repository, MutableInnoShopProductDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<bool> Handle(SetProductStatusCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetAsync(request.Id, cancellationToken);
            if (product == null)
            {
                return false;
            }
            product.IsActive = request.IsActive;
            await _repository.UpdateAsync(product, cancellationToken);
            return true;

        }
    }
}
