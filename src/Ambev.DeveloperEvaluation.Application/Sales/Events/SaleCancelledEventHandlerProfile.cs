using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.Events
{
    public class SaleCancelledEventHandlerProfile : Profile
    {
        public SaleCancelledEventHandlerProfile()
        {
            CreateMap<Sale, SaleCancelledEvent>();
        }
    }
}
