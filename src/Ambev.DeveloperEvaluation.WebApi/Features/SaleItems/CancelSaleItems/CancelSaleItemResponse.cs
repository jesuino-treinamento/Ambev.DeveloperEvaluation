namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.CancelSaleItems
{
    public class CancelSaleItemResponse
    {
        public Guid SaleId { get; set; }
        public Guid ItemId { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime CancellationDate { get; } = DateTime.UtcNow;
    }
}
