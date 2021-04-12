using AutoMapper;
using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Mapping
{
    public class MusicCatalogProfile : Profile
    {
        public MusicCatalogProfile()
        {
            CreateMap<RegisterSupplierDto, Supplier>()
                .ReverseMap();

            CreateMap<Album, AlbumDto>()
                .ForMember(dto => dto.Supplier, map => map.MapFrom(album => album.Supplier.Name))
                .ForMember(dto => dto.Artist, map => map.MapFrom(album => album.Artist.Name));
        }
    }
}
