using AutoMapper;
using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Mapping;
using System.ComponentModel.DataAnnotations;

namespace MusicCatalogAPI.Models.ArtistDtos
{
    public class CreateUpdateArtistDto : IMap
    {
        [Required]
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUpdateArtistDto, Artist>();
        }
    }
}
