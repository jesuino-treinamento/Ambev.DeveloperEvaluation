using Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItems;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales
{
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Customer ID is required")
                .Must(BeAValidGuid).WithMessage("Invalid Customer ID format");

            RuleFor(x => x.BranchId)
                .NotEmpty().WithMessage("Branch ID is required")
                .Must(BeAValidGuid).WithMessage("Invalid Branch ID format");

            RuleFor(x => x.Products)
                .NotEmpty().WithMessage("Sale must contain at least one item")
                .Must(items => items.Count <= 100).WithMessage("Sale cannot contain more than 100 items");

            RuleForEach(x => x.Products)
                .SetValidator(new CreateSaleItemCommandValidator());
        }

        private bool BeAValidGuid(Guid guid)
        {
            return guid != Guid.Empty;
        }
    }
}