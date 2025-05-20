using Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItems;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.CreateSaleItems;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales
{
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleRequest, CreateSaleCommand>();

            CreateMap<CreateSaleCommand, Sale>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Products))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => SaleStatus.Pending));

            CreateMap<CreateSaleItemCommand, SaleItem>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount));

            CreateMap<Sale, CreateSaleResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.SaleNumber, opt => opt.MapFrom(src => src.SaleNumber))
                .ForMember(dest => dest.SaleDate, opt => opt.MapFrom(src => src.SaleDate))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.TotalDiscount, opt => opt.MapFrom(src => src.TotalDiscount))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<SaleItem, CreateSaleItemResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                .ForMember(dest => dest.IsCancelled, opt => opt.MapFrom(src => src.IsCancelled));

            //CreateMap<Sale, GetSaleByIdResponse>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.SaleNumber, opt => opt.MapFrom(src => src.SaleNumber))
            //    .ForMember(dest => dest.SaleDate, opt => opt.MapFrom(src => src.SaleDate))
            //    .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            //    .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.User.ToString()))
            //    .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId))
            //    .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Name))
            //    .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
            //    .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
            //    .ForMember(dest => dest.TotalDiscount, opt => opt.MapFrom(src => src.TotalDiscount))
            //    .ForMember(dest => dest.IsCancelled, opt => opt.MapFrom(src => src.IsCancelled));

            //CreateMap<SaleItem, ItemResponse>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            //    .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            //    .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            //    .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
            //    .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
            //    .ForMember(dest => dest.IsCancelled, opt => opt.MapFrom(src => src.IsCancelled));

            //CreateMap<Sale, GetAllSalesResponse>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.SaleNumber, opt => opt.MapFrom(src => src.SaleNumber))
            //    .ForMember(dest => dest.SaleDate, opt => opt.MapFrom(src => src.SaleDate))
            //    .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            //    .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.User.ToString()))
            //    .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId))
            //    .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Name))
            //    .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
            //    .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
            //    .ForMember(dest => dest.TotalDiscount, opt => opt.MapFrom(src => src.TotalDiscount))
            //    .ForMember(dest => dest.IsCancelled, opt => opt.MapFrom(src => src.IsCancelled));

            CreateMap<CreateSaleRequest, CreateSaleCommand>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

            CreateMap<CreateSaleItemRequest, CreateSaleItemCommand>();

            CreateMap<CreateSaleResult, CreateSaleResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.SaleNumber, opt => opt.MapFrom(src => src.SaleNumber))
                .ForMember(dest => dest.SaleDate, opt => opt.MapFrom(src => src.SaleDate))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.TotalDiscount, opt => opt.MapFrom(src => src.TotalDiscount))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<CreateSaleItemResult, CreateSaleItemResponse>();

        }
    }
}

