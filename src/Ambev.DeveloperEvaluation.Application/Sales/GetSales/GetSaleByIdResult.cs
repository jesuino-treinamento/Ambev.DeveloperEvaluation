using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales
{
    public class GetSaleByIdResult
    {
        public Sale Sale { get; set; }

        public GetSaleByIdResult(Sale sale)
        {
            Sale = sale;
        }
    }
}
