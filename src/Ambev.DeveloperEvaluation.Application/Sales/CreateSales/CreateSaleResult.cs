using Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItems;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales
{
    public class CreateSaleResult
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Guid BranchId { get; set; }
        public string BranchName { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Subtotal { get; set; }
        public decimal TotalDiscount { get; set; }
       public List<CreateSaleItemResult> Items { get; set; } = new List<CreateSaleItemResult>();
       public string Status { get; set; }
    }
}
