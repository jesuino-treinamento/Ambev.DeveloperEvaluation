using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSale
{
    public class GetAllSalesQueryValidator : AbstractValidator<GetAllSalesQuery>
    {
        public GetAllSalesQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThan(0);
            RuleFor(x => x.Size).InclusiveBetween(1, 100);
        }
    }
}

