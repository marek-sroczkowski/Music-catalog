using AutoMapper;
using MusicCatalogAPI.Mapping;
using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Models.ArtistDtos;

namespace MusicCatalogAPI.Models.AlbumDtos
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
