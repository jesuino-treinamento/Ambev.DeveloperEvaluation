using Ambev.DeveloperEvaluation.Application.SaleItems.UpdateSaleItem;
using Ambev.DeveloperEvaluation.Application.Sales.ListSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.UpdateSaleItems;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSales
{
    public class UpdateSaleProfile : Profile
    {
        public UpdateSaleProfile()
        {
            CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
            CreateMap<UpdateSaleItemRequest, UpdateSaleItemCommand>();
            CreateMap<UpdateSaleResult, UpdateSaleResponse>();

            CreateMap<Sale, UpdateSaleRequest>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<Sale, UpdateSaleResponse>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
               .ForMember(dest => dest.TotalDiscount, opt => opt.MapFrom(src => src.TotalDiscount));

            CreateMap<Sale, GetSaleResult>();

        }
    }
}
