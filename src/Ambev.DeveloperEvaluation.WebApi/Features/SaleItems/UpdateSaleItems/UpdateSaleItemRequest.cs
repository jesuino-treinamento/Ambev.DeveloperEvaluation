namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.UpdateSaleItems
{
    public class UpdateSaleItemRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
