using Ambev.DeveloperEvaluation.Application.Sales.CancelSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSales
{
    public class CancelSaleProfile : Profile
    {
        public CancelSaleProfile()
        {
            CreateMap<CancelSaleResult, CancelSaleResponse>();

            CreateMap<CancelSaleCommand, Sale>();
            CreateMap<Sale, CancelSaleResponse>();
        }
    }
}
