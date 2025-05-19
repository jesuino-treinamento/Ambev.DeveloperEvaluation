using Ambev.DeveloperEvaluation.Application.SaleItems.GetSaleItems;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSale
{
    public class GetSaleResult
    {

        public Guid Id { get; set; }
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }

        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }

        public Guid BranchId { get; set; }
        public string BranchName { get; set; }

        public List<GetSaleItemResult> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public bool IsCancelled { get; set; }
    }
}
