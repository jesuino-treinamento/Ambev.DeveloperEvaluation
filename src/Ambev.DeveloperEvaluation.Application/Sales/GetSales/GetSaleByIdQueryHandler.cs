using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales
{
    public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQueryCommand, GetSaleByIdResult>
    {
        private readonly ISaleRepository _repository;
        private readonly ILogger<GetSaleByIdQueryHandler> _logger;

        public GetSaleByIdQueryHandler(ISaleRepository repository, ILogger<GetSaleByIdQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<GetSaleByIdResult> Handle(GetSaleByIdQueryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("🟢 Handler received query for SaleId: {request.Id}", request.SaleId);
            var sale = await _repository.GetByIdAsync(request.SaleId, cancellationToken);

            if (sale == null)
                throw new Exception("Sale not found");

            return new GetSaleByIdResult(sale);
        }
    }
}
