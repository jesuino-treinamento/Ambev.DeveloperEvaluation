using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.CancelSaleItems
{
    public class CancelSaleItemValidator : AbstractValidator<CancelSaleItemCommand>
    {
        public CancelSaleItemValidator()
        {
            RuleFor(x => x.ItemId).NotEmpty().WithMessage("SaleItemId is required.");
        }
    }
}
