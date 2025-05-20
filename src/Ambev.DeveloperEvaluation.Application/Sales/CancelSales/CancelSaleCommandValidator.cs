using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSales
{
    public class CancelSaleCommandValidator : AbstractValidator<CancelSaleCommand>
    {
        public CancelSaleCommandValidator()
        {
            RuleFor(x => x.SaleId)
                .NotEmpty().WithMessage("Sale ID is required");
        }
    }
}
