using AutoMapper;
using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Mapping;

namespace MusicCatalogAPI.Models.ArtistDtos
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
