using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.CancelSaleItems
{
    public class CancelSaleItemRequestValidator : AbstractValidator<CancelSaleItemRequest>
    {
        public CancelSaleItemRequestValidator()
        {
            RuleFor(x => x.SaleItemId).NotEmpty().WithMessage("SaleItemId is required.");
        }
    }
}
