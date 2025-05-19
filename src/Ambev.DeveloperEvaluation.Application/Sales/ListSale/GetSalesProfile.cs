using Ambev.DeveloperEvaluation.Application.SaleItems.GetSaleItems;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
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

            CreateMap<SaleItem, GetSaleItemResult>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Description))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Sale.Status.ToString()));
        }
    }
}
