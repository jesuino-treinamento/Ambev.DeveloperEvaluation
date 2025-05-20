namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSales
{
    public class CancelSaleRequest
    {
        public Guid Id { get; set; }
        public string CancellationReason { get; set; } = string.Empty;
    }
}
