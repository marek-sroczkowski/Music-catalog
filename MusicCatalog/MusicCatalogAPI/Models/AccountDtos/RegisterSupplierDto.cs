using AutoMapper;
using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Mapping;

namespace MusicCatalogAPI.Models.AccountDtos
{
    public class RegisterSupplierDto : IMap
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RegisterSupplierDto, Supplier>();
        }
    }
}
