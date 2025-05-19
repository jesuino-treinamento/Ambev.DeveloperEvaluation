using Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItems;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales
{
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleCommand, Sale>()
                .ForMember(dest => dest.SaleDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.IsCancelled, opt => opt.MapFrom(_ => false));

            CreateMap<User, CreateSaleResult>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Name.ToString()));

            CreateMap<CreateSaleItemCommand, SaleItem>()
                .ForMember(dest => dest.IsCancelled, opt => opt.MapFrom(_ => false));

            CreateMap<Sale, CreateSaleResult>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<SaleItem, CreateSaleItemResult>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.CalculateTotalAmount()));
        }
    }
}
