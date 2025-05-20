using Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.CreateSaleItems;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales
{
    public class CreateSaleResponse
    {
        public Guid Id { get; set; }
        public object SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }
        public Guid CustomerId { get; set; }
        public Guid BranchId { get; set; }
        public List<CreateSaleItemResponse> Items { get; set; } = new();
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public bool IsCancelled { get; set; }
    }
}
