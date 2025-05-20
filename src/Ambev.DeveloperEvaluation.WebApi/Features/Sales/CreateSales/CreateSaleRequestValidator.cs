using Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.CreateSaleItems;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales
{
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Customer is required");

            RuleFor(x => x.BranchId)
                .NotEmpty().WithMessage("Branch is required");

            RuleFor(x => x.Products)
                .NotEmpty().WithMessage("At least one item is required");

            RuleForEach(x => x.Products).SetValidator(new CreateSaleItemRequestValidator());
        }
    }
}
