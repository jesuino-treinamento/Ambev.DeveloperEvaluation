using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales
{
    public class GetSaleByIdQueryCommand : IRequest<GetSaleByIdResult>
    {
        public Guid SaleId { get; set; }
    }
}
