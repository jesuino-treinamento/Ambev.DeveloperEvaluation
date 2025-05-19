namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSales
{
    public class CancelSaleResult
    {
        public CancelSaleResult(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime CancellationDate { get; set; }
    }
}
