using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.CreateSaleItems
{
    public class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
    {
        public CreateSaleItemRequestValidator()
        {
            RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Product ID is required");

            RuleFor(x => x.Quantity).InclusiveBetween(1, 20)
                .GreaterThan(0).WithMessage("Quantity must be positive")
                .LessThanOrEqualTo(20).WithMessage("Cannot order more than 20 identical items");
        }
    }
}

