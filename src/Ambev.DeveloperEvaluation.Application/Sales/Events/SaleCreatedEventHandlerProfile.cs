using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events.Ambev.DeveloperEvaluation.Domain.Events;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.Events
{
    public class SaleCreatedEventHandlerProfile : Profile
    {
        public SaleCreatedEventHandlerProfile()
        {
            CreateMap<Sale, SaleCreatedEvent>();
        }
    }
}
