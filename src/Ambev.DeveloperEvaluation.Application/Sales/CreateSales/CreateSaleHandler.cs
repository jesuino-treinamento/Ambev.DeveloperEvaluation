using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Events.Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services.Interfaces;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IDiscountService _discountService;
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IPublisher _publisher;

        public CreateSaleHandler(
            ISaleRepository repository,
            IProductRepository productRepository,
            IDiscountService discountService,
            IMapper mapper,
            ICustomerRepository customerRepository,
            IBranchRepository branchRepository, IPublisher publisher)
        {
            _repository = repository;
            _productRepository = productRepository;
            _discountService = discountService;
            _mapper = mapper;
            _customerRepository = customerRepository;
            _branchRepository = branchRepository;
            _publisher = publisher;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
            var branch = await _branchRepository.GetByIdAsync(request.BranchId, cancellationToken);

            var items = new List<SaleItem>();

            foreach (var item in request.Products)
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

            var sale = new Sale(
                request.CustomerId,
                request.BranchId,
                items
            )
            {
                Status = request.IsCancelled ? SaleStatus.Cancelled : SaleStatus.Completed,
            };

            sale.CalculateTotals();
            await _repository.CreateAsync(sale, cancellationToken);


            var eventToPublish = new SaleCreatedEvent(sale.Id, sale.SaleNumber, sale.CustomerId, sale.TotalAmount);

            await _publisher.Publish(eventToPublish, cancellationToken);


            var result = _mapper.Map<CreateSaleResult>(sale);
            result.CustomerName = customer?.ToString();
            result.BranchName = branch?.Name;

            return result;
        }
    }
}
