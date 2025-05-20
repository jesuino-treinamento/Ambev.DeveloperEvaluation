using Ambev.DeveloperEvaluation.Application.SaleItems.CancelSaleItems;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.CancelSaleItems
{
    public class CancelSaleItemProfile : Profile
    {
        public CancelSaleItemProfile()
        {
            CreateMap<CancelSaleItemRequest, CancelSaleItemCommand>()
                .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.SaleItemId));

            CreateMap<CancelSaleItemResult, CancelSaleItemResponse>();
        }
    }
}
