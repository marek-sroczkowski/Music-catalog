using AutoMapper;
using WebApi.Entities;
using WebApi.Mapping;

namespace WebApi.Models.SongDtos
{
    public class SongDto : IMap
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PublicationYear { get; set; }
        public double Duration { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Song, SongDto>();
        }
    }
}