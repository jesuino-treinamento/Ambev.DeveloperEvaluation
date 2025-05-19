using Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItems;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales
{
    public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleValidator()
        {
            RuleFor(x => x.SaleNumber).NotEmpty();
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.BranchId).NotEmpty();
            RuleFor(x => x.Products).NotEmpty();
            RuleForEach(x => x.Products).SetValidator(new CreateSaleItemValidator());
        }
    }
}
