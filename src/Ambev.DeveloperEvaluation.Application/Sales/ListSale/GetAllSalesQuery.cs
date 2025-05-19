
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common.Pagination;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSale
{
    public class GetAllSalesQuery : IRequest<PaginatedList<GetSaleResult>>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
        public string Order { get; set; } = "saledate, total_amount";

        public ValidationResultDetail Validate()
        {
            return new ValidationResultDetail(new GetAllSalesQueryValidator().Validate(this));
        }
    }
}
