using Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.CreateSaleItems;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales
{
    public class CreateSaleRequest
    {
        public Guid CustomerId { get; set; }

        public Guid BranchId { get; set; }
        public DateTime SaleDate { get; set; }
        public List<CreateSaleItemRequest> Products { get; set; }
    }
}
