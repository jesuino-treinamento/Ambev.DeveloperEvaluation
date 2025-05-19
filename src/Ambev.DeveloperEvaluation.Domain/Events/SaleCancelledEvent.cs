using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleCancelledEvent : INotification
    {
        private Guid id;
        private DateTime? cancellationDate;

        public Guid SaleId { get; }
        public DateTime CancelledAt { get; } = DateTime.UtcNow;

        public SaleCancelledEvent(Guid saleId, DateTime cancelledAt)
        {
            SaleId = saleId;
            CancelledAt = cancelledAt;
        }

        public SaleCancelledEvent(Guid id, DateTime? cancellationDate)
        {
            this.id = id;
            this.cancellationDate = cancellationDate;
        }
    }
}
