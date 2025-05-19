namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales
{
    public class GetSaleResponse
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; } = default!;
        public DateTime SaleDate { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = default!;
        public Guid BranchId { get; set; }
        public string BranchName { get; set; } = default!;
        public List<ItemResponse> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public bool IsCancelled { get; set; }
    }
}
