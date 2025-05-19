namespace Ambev.DeveloperEvaluation.Application.SaleItems.CancelSaleItems
{
    public class CancelSaleItemResult
    {
        public Guid SaleId { get; set; }
        public Guid ItemId { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime CancellationDate { get; set; } = DateTime.UtcNow;
       
    }
}
