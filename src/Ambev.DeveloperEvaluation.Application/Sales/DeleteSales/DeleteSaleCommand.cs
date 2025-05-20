using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSales
{
    public class DeleteSaleCommand : IRequest<DeleteSaleResult>
    {
        public DeleteSaleCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
