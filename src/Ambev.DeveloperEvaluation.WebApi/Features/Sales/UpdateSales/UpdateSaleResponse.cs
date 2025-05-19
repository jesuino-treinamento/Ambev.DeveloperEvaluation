namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSales
{
    public class UpdateSaleResponse
    {
        public Guid Id { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public bool IsCancelled { get; set; }
    }
}
