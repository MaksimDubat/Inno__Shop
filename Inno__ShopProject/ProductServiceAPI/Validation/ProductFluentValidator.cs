using FluentValidation;
using ProductServiceAPI.Entities;

namespace ProductServiceAPI.Validation
{
    public class ProductFluentValidator : AbstractValidator<Product>
    {
        public ProductFluentValidator()
        {
            RuleFor(p => p.Id)
                .Null();
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("need name")
                .MaximumLength(100).WithMessage("product cant ");
            RuleFor(p => p.Description)
                .MaximumLength(500).WithMessage("no longer than 500");
            RuleFor(p => p.Price)
                .GreaterThanOrEqualTo(0).WithMessage("no products");
            RuleFor(p => p.UserId)
                .Null();
            RuleFor(p => p.IsAvaliable)
                .NotNull();
            RuleFor(p => p.CreatedAt)
                .GreaterThanOrEqualTo(DateTime.UtcNow.AddDays(-10));
        }
    }
}
