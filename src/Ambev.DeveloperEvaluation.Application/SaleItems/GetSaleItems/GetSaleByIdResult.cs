using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.GetSaleItems
{
    public class GetSaleByIdResult
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }
        public Guid CustomerId { get; set; }
        public Guid BranchId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;

        public List<GetSaleItemResult> Items { get; set; } = new();
        public string CustomerName { get; set; }
        public string BranchName { get; set; }
        public Sale Sale { get; set; }
    }
}
