using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser
{
    public class CreateUserProfile : Profile
    {
        public CreateUserProfile()
        {
            CreateMap<CreateUserCommand, User>()
                   .ForMember(dest => dest.Password, opt => opt.Ignore())
                   .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                   .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<User, CreateUserResult>();
            CreateMap<Name, NameModelResult>();
            CreateMap<Address, AddressModelResult>();
            CreateMap<Geolocation, GeolocationModelResult>();
        }
    }
}
