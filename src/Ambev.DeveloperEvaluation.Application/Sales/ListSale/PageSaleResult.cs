namespace Ambev.DeveloperEvaluation.Application.Sales.ListSale
{
    public class PageSaleResult
    {
        public IEnumerable<GetSaleResult> Data { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
