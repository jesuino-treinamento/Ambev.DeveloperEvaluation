using Ambev.DeveloperEvaluation.Application.Sales.DeleteSales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSales
{
    public class DeleteSaleProfile : Profile
    {
        public DeleteSaleProfile()
        {
            CreateMap<DeleteSaleRequest, DeleteSaleCommand>();
            CreateMap<DeleteSaleResult, DeleteSaleResponse>();
        }
    }
}
