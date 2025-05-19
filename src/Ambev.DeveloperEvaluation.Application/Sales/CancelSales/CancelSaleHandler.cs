using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSales
{
    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IPublisher _publisher;

        public CancelSaleHandler(ISaleRepository saleRepository, IPublisher publisher)
        {
            _saleRepository = saleRepository;
            _publisher = publisher;
        }

        public async Task<CancelSaleResult> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new CancelSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            //if (!validationResult.IsValid)
            //  throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetByIdAsync(request.SaleId, cancellationToken);
            if (sale == null)
                throw new Exception("Sale not found.");

            sale.CancelSale();

            await _saleRepository.UpdateAsync(sale, cancellationToken);

            var eventToPublish = new SaleCancelledEvent(sale.Id, sale.CancellationDate);

            await _publisher.Publish(eventToPublish, cancellationToken);

            return new CancelSaleResult(sale.Id);
        }
    }
}
