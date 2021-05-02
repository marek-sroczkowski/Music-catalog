using AutoMapper;
using WebApi.Entities;
using WebApi.Mapping;

namespace WebApi.Models.ArtistDtos
{
    public class ArtistDto : IMap
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Artist, ArtistDto>();
        }
    }
}
