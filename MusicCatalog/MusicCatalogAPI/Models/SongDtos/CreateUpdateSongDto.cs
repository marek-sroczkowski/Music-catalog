using AutoMapper;
using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Mapping;
using System.ComponentModel.DataAnnotations;

namespace MusicCatalogAPI.Models.SongDtos
{
    public class CreateUpdateSongDto : IMap
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int PublicationYear { get; set; }

        [Required]
        public double Duration { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUpdateSongDto, Song>();
        }
    }
}
