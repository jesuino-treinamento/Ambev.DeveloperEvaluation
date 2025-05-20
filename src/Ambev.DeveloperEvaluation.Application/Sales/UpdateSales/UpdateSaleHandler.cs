using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSales
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;
        private readonly IDiscountService _discountService;
        private readonly IMapper _mapper;
        private readonly IPublisher _publisher;
        private readonly ILogger<UpdateSaleHandler> _logger;

        public UpdateSaleHandler(ISaleRepository saleRepository, IProductRepository productRepository,
            IDiscountService discountService, IMapper mapper, IPublisher publisher, ILogger<UpdateSaleHandler> logger)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _discountService = discountService;
            _mapper = mapper;
            _publisher = publisher;
            _logger = logger;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

            if (sale == null)
                throw new KeyNotFoundException("Sale not found");

            var items = new List<SaleItem>();
            
            sale.Items.Clear();

            foreach (var item in request.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId, cancellationToken);

                if (product == null)
                    throw new ArgumentException($"Product not found for ID {item.ProductId}");

                var discount = _discountService.CalculateDiscount(item.Quantity, product.Price);

                items.Add(new SaleItem(
                    item.ProductId,
                    product.Description,
                    product.Price,
                    item.Quantity,
                    discount
                ));
            }

            sale.Items = items;
            sale.Status = SaleStatus.Completed;

            sale.CalculateTotals(); ;

            await _saleRepository.UpdateAsync(sale, cancellationToken);

            var eventToPublish = new SaleModifiedEvent(sale.Id, sale.SaleDate);

            await _publisher.Publish(eventToPublish, cancellationToken);


            return new UpdateSaleResult
            {
                Id = sale.Id,
                TotalAmount = sale.TotalAmount,
                TotalDiscount = sale.TotalDiscount,
                IsCancelled = sale.IsCancelled
            };
        }
    }
}
