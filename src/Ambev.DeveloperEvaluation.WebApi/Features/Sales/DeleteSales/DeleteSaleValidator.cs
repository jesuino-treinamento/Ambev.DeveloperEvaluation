using Ambev.DeveloperEvaluation.Application.Sales.DeleteSales;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSales
{
    public class DeleteSaleValidator : AbstractValidator<DeleteSaleCommand>
    {
        public DeleteSaleValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("SaleId is required.");
        }
    }
}
