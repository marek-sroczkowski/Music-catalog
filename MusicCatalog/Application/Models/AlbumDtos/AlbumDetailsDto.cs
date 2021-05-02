using AutoMapper;
using WebApi.Entities;
using WebApi.Mapping;
using WebApi.Models.ArtistDtos;
using WebApi.Models.SongDtos;
using WebApi.Models.SupplierDtos;
using System.Collections.Generic;

namespace WebApi.Models.AlbumDtos
{
    public class AlbumDetailsDto : IMap
    {
        public int Id { get; set; }
        public ArtistDto Artist { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public int PublicationYear { get; set; }
        public SupplierDto Supplier { get; set; }
        public List<SongDto> Songs{ get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Album, AlbumDetailsDto>();
        }
    }
}
