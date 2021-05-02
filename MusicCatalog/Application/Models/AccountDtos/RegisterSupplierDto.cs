using AutoMapper;
using WebApi.Entities;
using WebApi.Mapping;

namespace WebApi.Models.AccountDtos
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
