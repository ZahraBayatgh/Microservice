using Authentication.Identity;
using AutoMapper;
using Identity.API.Models;

namespace dentity.API.Data
{
    public class AuthMaps : Profile
    {
        public AuthMaps()
        {
            CreateMap<CustomIdentityUser, CustomIdentityModel>()
              .ReverseMap();
        }
    }
}
