using Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.UpdateSaleItems;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSales
{
    public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
    {
        public UpdateSaleRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleForEach(x => x.Items).SetValidator(new UpdateSaleItemValidator());
        }        
    }
}
