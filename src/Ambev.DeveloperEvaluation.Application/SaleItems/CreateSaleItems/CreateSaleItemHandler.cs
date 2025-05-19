using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItems
{
    public class CreateSaleItemHandler : IRequestHandler<CreateSaleItemCommand, CreateSaleItemResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ISaleItemRepository _saleItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IDiscountService _discountService;
        private readonly ILogger<CreateSaleItemHandler> _logger;

        public CreateSaleItemHandler(
            ISaleRepository saleRepository,
            IProductRepository productRepository,
            IDiscountService discountService,
            ILogger<CreateSaleItemHandler> logger)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _discountService = discountService;
            _logger = logger;
        }

        public async Task<CreateSaleItemResult> Handle(CreateSaleItemCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetSaleItemByIdAsync(request.SaleId, cancellationToken);
            if (sale == null)
                throw new ArgumentException("Sale not found");

            var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
            if (product == null)
                throw new ArgumentException("Product not found");

            var discount = _discountService.CalculateDiscount(request.Quantity, product.Price);

            var saleItem = new SaleItem(
                product.Id,
                product.Description,
                product.Price,
                request.Quantity,
                discount
            );

            sale.Sale.AddItem(saleItem);

            await _saleItemRepository.CreateAsync(saleItem, cancellationToken);

            _logger.LogInformation("Item added to sale {SaleId}: {ProductId} x{Quantity}", request.SaleId, request.ProductId, request.Quantity);

            return new CreateSaleItemResult
            {
                Id = saleItem.Id,
                ProductId = saleItem.ProductId,
                ProductName = saleItem.ProductName,
                UnitPrice = saleItem.UnitPrice,
                Quantity = saleItem.Quantity,
                Discount = saleItem.Discount,
                TotalPrice = saleItem.TotalPrice
            };
        }
    }
}
