using Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItems;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.CreateSaleItems
{
    public class CreateSaleItemProfile : Profile
    {
        public CreateSaleItemProfile()
        {
            CreateMap<CreateSaleItemRequest, SaleItem>(); 
            CreateMap<SaleItem, CreateSaleItemResult>();  
        }
    }
}
