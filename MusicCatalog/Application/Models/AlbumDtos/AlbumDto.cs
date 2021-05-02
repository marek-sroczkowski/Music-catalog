using AutoMapper;
using WebApi.Mapping;
using WebApi.Entities;
using WebApi.Models.ArtistDtos;

namespace WebApi.Models.AlbumDtos
{
    public class AlbumDto : IMap
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PublicationYear { get; set; }
        public string Version { get; set; }
        public string Supplier { get; set; }
        public ArtistDto Artist { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Album, AlbumDto>()
                .ForMember(dto => dto.Supplier, map => map.MapFrom(album => album.Supplier.Name));
        }
    }
}
