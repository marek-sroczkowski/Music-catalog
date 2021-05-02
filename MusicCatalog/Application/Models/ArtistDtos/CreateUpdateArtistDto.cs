using AutoMapper;
using WebApi.Entities;
using WebApi.Mapping;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.ArtistDtos
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
