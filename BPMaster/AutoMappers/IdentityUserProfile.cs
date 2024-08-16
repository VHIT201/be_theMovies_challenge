using System.Security.Principal;
using Common.Application.Models;
using Common.Mappers.AutoMapper;
using Domain.Dtos;
using Domain.Entities;

namespace AutoMappers 
{
    public class IdentityUserProfile : BaseProfile
    {
        protected override void CreateMaps()
        {
            CreateMap<RegisterUserDto, IdentityUser>()
                .ForMember(x => x.Password, opt => opt.Ignore());

            CreateMap<IdentityUser, AuthenticatedUserModel>()
                .ForMember(x => x.UserId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
