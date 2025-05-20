using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.Events
{
    public class ItemCancelledEventHandlerProfile : Profile
    {
        public ItemCancelledEventHandlerProfile()
        {
            CreateMap<SaleItem, ItemCancelledEvent>();
        }
    }
}
