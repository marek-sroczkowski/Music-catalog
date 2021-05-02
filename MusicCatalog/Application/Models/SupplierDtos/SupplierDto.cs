using AutoMapper;
using WebApi.Entities;
using WebApi.Mapping;

namespace WebApi.Models.SupplierDtos
{
    public class SupplierDto : IMap
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Supplier, SupplierDto>();
        }
    }
}
