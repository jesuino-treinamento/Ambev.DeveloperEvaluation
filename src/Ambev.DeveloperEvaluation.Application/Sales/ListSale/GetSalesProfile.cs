using Ambev.DeveloperEvaluation.Application.SaleItems.GetSaleItems;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSale
{
    public class GetSalesProfile : Profile
    {
        public GetSalesProfile()
        {
            CreateMap<Sale, GetSaleResult>()
                 .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
                 .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Name))
                 .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<SaleItem, GetSaleItemResult>();
        }
    }
}
