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
        }
    }
}
