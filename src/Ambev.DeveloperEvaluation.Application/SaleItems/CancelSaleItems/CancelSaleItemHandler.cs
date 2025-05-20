using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.CancelSaleItems
{
    public class CancelSaleItemHandler : IRequestHandler<CancelSaleItemCommand, CancelSaleItemResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IPublisher _publisher;

        public CancelSaleItemHandler(ISaleRepository saleRepository, IPublisher publisher)
        {
            _saleRepository = saleRepository;
            _publisher = publisher;
        }

        public async Task<CancelSaleItemResult> Handle(CancelSaleItemCommand request, CancellationToken cancellationToken)
        {
            var saleItem = await _saleRepository.GetSaleId_SaleItemByIdAsync(request.SaleId, request.ItemId, cancellationToken);
            var sale = await _saleRepository.GetByIdAsync(request.SaleId, cancellationToken);

            if (saleItem == null)
                throw new KeyNotFoundException("Sale item not found.");

            await _saleRepository.CancelSaleItemAsync(sale, request.ItemId, cancellationToken);
            await _saleRepository.UpdateSaleItemAsync(saleItem, cancellationToken);

            var eventToPublish = new ItemCancelledEvent(saleItem.SaleId, saleItem.Id);

            await _publisher.Publish(eventToPublish, cancellationToken);

            return new CancelSaleItemResult
            {
                SaleId = saleItem.SaleId,
                ItemId = saleItem.Id,
                IsCancelled = saleItem.IsCancelled,
                //CancellationDate = saleItem.CancellationDate
                CancellationDate = saleItem.CancellationDate ?? DateTime.UtcNow
            };
        }
    }
}
