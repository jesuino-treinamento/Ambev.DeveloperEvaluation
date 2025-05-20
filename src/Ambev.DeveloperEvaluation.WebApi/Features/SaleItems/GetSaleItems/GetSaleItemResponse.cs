namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.GetSaleItems
{
    public class GetSaleItemResponse
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
