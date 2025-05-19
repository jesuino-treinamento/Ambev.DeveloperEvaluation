using Ambev.DeveloperEvaluation.Application;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

public class GetUserProfile : Profile
{
    public GetUserProfile()
    {       
        CreateMap<GetUserResult, GetUserResponse>();
        CreateMap<NameModelResult, NameModel>();
        CreateMap<AddressModelResult, AddressModel>();
        CreateMap<GeolocationModelResult, GeolocationModel>();

    }
}
