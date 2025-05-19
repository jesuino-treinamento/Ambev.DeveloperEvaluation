using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.Events
{
    public class SaleModifiedEventHandlerProfile : Profile
    {
        public SaleModifiedEventHandlerProfile()
        {
            CreateMap<Sale, SaleModifiedEvent>();
        }
    }
}
