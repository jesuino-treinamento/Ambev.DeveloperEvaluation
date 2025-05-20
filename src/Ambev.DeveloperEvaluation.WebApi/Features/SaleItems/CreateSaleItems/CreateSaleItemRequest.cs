namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.CreateSaleItems
{
    public class CreateSaleItemRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
