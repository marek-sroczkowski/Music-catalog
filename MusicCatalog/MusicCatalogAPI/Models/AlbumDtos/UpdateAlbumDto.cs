using AutoMapper;
using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Mapping;
using System.ComponentModel.DataAnnotations;

namespace MusicCatalogAPI.Models.AlbumDtos
{
    public class UpdateAlbumDto : IMap
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public int PublicationYear { get; set; }

        public string Version { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateAlbumDto, Album>();
        }
    }
}
