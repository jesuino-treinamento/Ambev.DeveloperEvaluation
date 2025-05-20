using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser
{
    public sealed class AuthenticateUserProfile : Profile
    {
        public AuthenticateUserProfile()
        {
            CreateMap<User, AuthenticateUserResult>()
                .ForMember(dest => dest.Token, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
        }
    }
}
