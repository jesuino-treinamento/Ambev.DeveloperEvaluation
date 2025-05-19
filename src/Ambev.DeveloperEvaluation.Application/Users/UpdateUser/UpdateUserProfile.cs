using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser
{
    public class UpdateUserProfile : Profile
    {
        public UpdateUserProfile()
        {
            CreateMap<User, UpdateUserResult>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                    new NameModelResult
                    {
                        FirstName = src.Name.FirstName,
                        LastName = src.Name.LastName
                    }))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src =>
                    new AddressModelResult
                    {
                        City = src.Address.City,
                        Street = src.Address.Street,
                        Number = src.Address.Number,
                        ZipCode = src.Address.ZipCode,
                        Geolocation = new GeolocationModelResult
                        {
                            Lat = src.Address.Geolocation.Lat,
                            Long = src.Address.Geolocation.Long
                        }
                    }));
        }
    }
}

