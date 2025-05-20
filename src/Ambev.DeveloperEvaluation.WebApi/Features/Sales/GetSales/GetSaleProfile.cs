using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales
{
    public class GetSaleProfile : Profile
    {
        public GetSaleProfile()
        {
            CreateMap<GetSaleByIdResult, GetSaleResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Sale.Id))
                .ForMember(dest => dest.SaleNumber, opt => opt.MapFrom(src => src.Sale.SaleNumber))
                .ForMember(dest => dest.SaleDate, opt => opt.MapFrom(src => src.Sale.SaleDate))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Sale.CustomerId))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Sale.Customer.Name))
                .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.Sale.BranchId))
                .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Sale.Branch.Name))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Sale.TotalAmount))
                .ForMember(dest => dest.TotalDiscount, opt => opt.MapFrom(src => src.Sale.TotalDiscount))
                .ForMember(dest => dest.IsCancelled, opt => opt.MapFrom(src => src.Sale.Status == SaleStatus.Cancelled))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Sale.Items));

            CreateMap<SaleItem, ItemResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Description))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                .ForMember(dest => dest.IsCancelled, opt => opt.MapFrom(src => src.IsCancelled));
        }
    }
}
