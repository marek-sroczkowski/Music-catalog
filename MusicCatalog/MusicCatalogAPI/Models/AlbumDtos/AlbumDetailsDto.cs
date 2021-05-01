using AutoMapper;
using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Mapping;
using MusicCatalogAPI.Models.ArtistDtos;
using MusicCatalogAPI.Models.SongDtos;
using System.Collections.Generic;

namespace MusicCatalogAPI.Models.AlbumDtos
{
    public class AlbumDetailsDto : IMap
    {
        public int Id { get; set; }
        public ArtistDto Artist { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public int PublicationYear { get; set; }
        public string Supplier { get; set; }
        public List<SongDto> Songs{ get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Album, AlbumDetailsDto>()
                .ForMember(dto => dto.Supplier, map => map.MapFrom(album => album.Supplier.Name));
        }
    }
}
