using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser
{
    public class UpdateUserProfile : Profile
    {
        public UpdateUserProfile()
        {           
            CreateMap<UpdateUserRequest, UpdateUserCommand>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                new Name { FirstName = src.Name.FirstName, LastName = src.Name.LastName }))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src =>
                new Address
                {
                    City = src.Address.City,
                    Street = src.Address.Street,
                    Number = src.Address.Number,
                    ZipCode = src.Address.ZipCode,
                    Geolocation = new Geolocation
                    {
                        Lat = src.Address.Geolocation.Lat,
                        Long = src.Address.Geolocation.Long
                    }
                }))
             .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
             .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
             .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));

            CreateMap<UpdateUserResult, UpdateUserResponse>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                    .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new NameModel
                    {
                        FirstName = src.Name.FirstName,
                        LastName = src.Name.LastName
                    }))
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new AddressModel
                    {
                        City = src.Address.City,
                        Street = src.Address.Street,
                        Number = src.Address.Number,
                        ZipCode = src.Address.ZipCode,
                        Geolocation = new GeolocationModel
                        {
                            Lat = src.Address.Geolocation.Lat,
                            Long = src.Address.Geolocation.Long
                        }
                    }))
                    .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                    .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

        }
    }
}
