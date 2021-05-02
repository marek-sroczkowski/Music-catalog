using AutoMapper;
using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Mapping;

namespace MusicCatalogAPI.Models.AccountDtos
{
    public class UserDto : IMap
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public Role Role { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDto>();
        }
    }
}
