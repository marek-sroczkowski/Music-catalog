using AutoMapper;
using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Models;
using MusicCatalogAPI.Models.Album;
using MusicCatalogAPI.Models.Artist;
using MusicCatalogAPI.Models.Song;

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

            CreateMap<CreateUpdateArtistDto, Artist>()
                .ReverseMap();

            CreateMap<Album, AlbumDto>()
                .ForMember(dto => dto.Supplier, map => map.MapFrom(album => album.Supplier.Name))
                .ReverseMap();

            CreateMap<SongDto, Song>()
                .ReverseMap();

            CreateMap<CreateUpdateSongDto, Song>()
                .ReverseMap();

            CreateMap<Album, AlbumDetailsDto>()
                .ForMember(dto => dto.Supplier, map => map.MapFrom(album => album.Supplier.Name));

            CreateMap<CreateUpdateAlbumDto, Album>()
                .ReverseMap();
        }
    }
}
