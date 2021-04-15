using AutoMapper;
using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Models;

namespace MusicCatalogAPI.Mapping
{
    public class MusicCatalogProfile : Profile
    {
        public MusicCatalogProfile()
        {
            CreateMap<RegisterSupplierDto, Supplier>()
                .ReverseMap();

            CreateMap<Artist, ArtistDto>()
                .ReverseMap();

            CreateMap<Album, AlbumDto>()
                .ForMember(dto => dto.Supplier, map => map.MapFrom(album => album.Supplier.Name))
                .ReverseMap();

            CreateMap<SongDto, Song>()
                .ReverseMap();

            CreateMap<Album, AlbumDetailsDto>()
                .ForMember(dto => dto.Supplier, map => map.MapFrom(album => album.Supplier.Name))
                .ForMember(dto => dto.Artist, map => map.MapFrom(album => album.Artist.Name));
        }
    }
}
