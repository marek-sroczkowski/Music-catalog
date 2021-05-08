using AutoMapper;
using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Mapping;

namespace MusicCatalogAPI.Models.AccountDtos
{
    public class LoginUserDto : IMap
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<LoginUserDto, User>();
        }
    }
}
