namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.CancelSaleItems
{
    public class CancelSaleItemRequest
    {
        public Guid SaleId { get; set; }
        public Guid SaleItemId { get; set; }
    }
}
