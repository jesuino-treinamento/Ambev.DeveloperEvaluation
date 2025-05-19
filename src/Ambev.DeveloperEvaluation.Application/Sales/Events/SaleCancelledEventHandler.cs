using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.Events
{
    public class SaleCancelledEventHandler : INotificationHandler<SaleCancelledEvent>
    {
        private readonly ILogger<SaleCancelledEventHandler> _logger;

        public SaleCancelledEventHandler(ILogger<SaleCancelledEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(SaleCancelledEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogWarning("🔴 Sale Cancelled Event: {SaleId}", notification.SaleId);
            return Task.CompletedTask;
        }
    }
}
