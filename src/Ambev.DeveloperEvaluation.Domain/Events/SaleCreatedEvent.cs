using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    namespace Ambev.DeveloperEvaluation.Domain.Events
    {
        public class SaleCreatedEvent : INotification
        {
            public Guid SaleId { get; }
            public string SaleNumber { get; }
            public Guid CustomerId { get; }
            public decimal TotalAmount { get; }

            public SaleCreatedEvent(Guid saleId, string saleNumber, Guid customerId, decimal totalAmount)
            {
                SaleId = saleId;
                SaleNumber = saleNumber;
                CustomerId = customerId;
                TotalAmount = totalAmount;
            }
        }
    }
}
