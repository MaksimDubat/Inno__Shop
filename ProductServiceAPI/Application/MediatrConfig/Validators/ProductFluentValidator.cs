using FluentValidation;
using ProductServiceAPI.Application.MediatrConfig.Commands;
using ProductServiceAPI.Domain.Entities;

namespace ProductServiceAPI.Application.MediatrConfig.Validators
{
    /// <summary>
    /// Класс валидатора для валидации продкутов.
    /// </summary>
    public class ProductFluentValidator : AbstractValidator<AddProductCommand>
    {
        public ProductFluentValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("need name")
                .MaximumLength(100).WithMessage("product cant ");
            RuleFor(p => p.Description)
                .MaximumLength(500).WithMessage("no longer than 500");
            RuleFor(p => p.Price)
                .GreaterThanOrEqualTo(1).WithMessage("no products with that price");
            RuleFor(p => p.UserID)
                .NotEmpty().WithMessage("imput");
        }
    }
}
