using Ambev.DeveloperEvaluation.Application.SaleItems.UpdateSaleItem;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSales
{
    public class UpdateSaleCommand : IRequest<UpdateSaleResult>
    {
        public Guid Id { get; set; }
        public DateTime SaleDate { get; set; }
        public List<UpdateSaleItemCommand> Items { get; set; } = new();
    }
}
