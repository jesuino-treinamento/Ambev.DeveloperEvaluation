namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSales
{
    public class CancelSaleResponse
    {
        public Guid Id { get; set; }
        public DateTime CancelledAt { get; } = DateTime.UtcNow;
    }
}
