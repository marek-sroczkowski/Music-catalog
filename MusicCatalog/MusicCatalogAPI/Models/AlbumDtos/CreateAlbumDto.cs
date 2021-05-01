using AutoMapper;
using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Mapping;
using MusicCatalogAPI.Models.ArtistDtos;
using System.ComponentModel.DataAnnotations;

namespace MusicCatalogAPI.Models.AlbumDtos
{
    public class CreateAlbumDto : IMap
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public int PublicationYear { get; set; }

        [Required]
        public CreateUpdateArtistDto Artist { get; set; }

        public string Version { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateAlbumDto, Album>();
        }
    }
}
