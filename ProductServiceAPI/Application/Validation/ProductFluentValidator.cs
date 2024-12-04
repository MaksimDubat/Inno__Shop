using FluentValidation;
using ProductServiceAPI.Domain.Entities;

namespace ProductServiceAPI.Validation
{
    /// <summary>
    /// Класс валидатора для валидации продкутов.
    /// </summary>
    public class ProductFluentValidator : AbstractValidator<Product>
    {
        public ProductFluentValidator()
        {
            RuleFor(p => p.Id)
                .NotNull().WithMessage("need id");
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("need name")
                .MaximumLength(100).WithMessage("product cant ");
            RuleFor(p => p.Description)
                .MaximumLength(500).WithMessage("no longer than 500");
            RuleFor(p => p.Price)
                .GreaterThanOrEqualTo(1).WithMessage("no products");
            RuleFor(p => p.UserId)
                .NotNull().WithMessage("need user id");
            RuleFor(p => p.IsAvaliable)
                .NotNull();
            RuleFor(p => p.CreatedAt)
                .GreaterThanOrEqualTo(DateTime.UtcNow.AddDays(-10));
        }
    }
}
