using Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.UpdateSaleItems;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSales
{
    public class UpdateSaleRequest
    {
        public Guid Id { get; internal set; }
        public DateTime SaleDate { get; set; }
        public List<UpdateSaleItemRequest> Items { get; set; } = new();
    }   
}
