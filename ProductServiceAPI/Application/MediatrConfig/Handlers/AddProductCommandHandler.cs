using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using ProductServiceAPI.Application.MediatrConfig.Commands;
using ProductServiceAPI.Domain.Entities;
using ProductServiceAPI.Interface;

namespace ProductServiceAPI.Application.MediatrConfig.Hendlers
{
    /// <summary>
    /// Обработчик команды добавления продукта.
    /// </summary>
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, string>
    {
        private readonly IProductRepository _repository;
        private readonly IValidator<AddProductCommand> _validator;
        public AddProductCommandHandler(IProductRepository repository, IValidator<AddProductCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }
        public async Task<string> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var validation = await _validator.ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                UserId = request.UserID,
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                IsAvaliable = true
            };
            await _repository.AddAsync(product, cancellationToken);
            return $"Product '{product.Name}' added";
        }
    }
}
