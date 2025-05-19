using Ambev.DeveloperEvaluation.Domain.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSale
{
    public class GetAllSaleHandler : IRequestHandler<GetAllSalesQuery, PaginatedList<GetSaleResult>>
    {

        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public GetAllSaleHandler(ISaleRepository saleRepository, IMapper mapper)
            => (_saleRepository, _mapper) = (saleRepository, mapper);

        public async Task<PaginatedList<GetSaleResult>> Handle(
            GetAllSalesQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {

                var salesPaginated = await _saleRepository.GetAllPaginatedAsync(
                   request.Page,
                   request.Size,
                   request.Order);

                var results = _mapper.Map<List<GetSaleResult>>(salesPaginated.Items);

                return new PaginatedList<GetSaleResult>(
                    results,
                    salesPaginated.TotalCount,
                    salesPaginated.PageNumber,
                    salesPaginated.PageSize);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
           
        }
    }
}