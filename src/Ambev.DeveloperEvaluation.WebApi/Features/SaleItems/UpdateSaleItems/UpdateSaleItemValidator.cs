using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.UpdateSaleItems
{
    public class UpdateSaleItemValidator : AbstractValidator<UpdateSaleItemRequest>
    {
        public UpdateSaleItemValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}
