using FluentValidation;
using MediatR;
using ProductServiceAPI.Application.MediatrConfig.Commands;
using ProductServiceAPI.DataBase;
using ProductServiceAPI.Domain.Entities;
using ProductServiceAPI.Infrastructure.Common;
using ProductServiceAPI.Interface;

namespace ProductServiceAPI.Application.MediatrConfig.Hendlers
{
    /// <summary>
    /// Обработчик команды добавления продукта.
    /// </summary>
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly IProductRepository _repository;
        private readonly IValidator<UpdateProductCommand> _validator;

        public UpdateProductCommandHandler(IProductRepository repository, IValidator<UpdateProductCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }
        public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validation = await _validator.ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }
            var product = new Product
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                IsActive = request.IsActive
            };

            var updatedProduct = await _repository.UpdateAsync(product, cancellationToken);
            return updatedProduct;
        }
    }
}
