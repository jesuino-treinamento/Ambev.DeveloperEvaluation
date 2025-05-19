namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSales
{
    public class DeleteSaleResult
    {
        public DeleteSaleResult(Guid id, bool is_deleted)
        {
            Id = id;
            Is_Deleted = is_deleted;
        }

        public Guid Id { get; set; }
        public bool Is_Deleted { get; set; }
    }
}
