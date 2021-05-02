using AutoMapper;
using MusicCatalogAPI.Filters;
using MusicCatalogAPI.Interfaces;
using MusicCatalogAPI.Models.AlbumDtos;
using MusicCatalogAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Helpers;

namespace MusicCatalogAPI.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IMapper _mapper;

        public AlbumService(IAlbumRepository albumRepository, IMapper mapper)
        {
            _albumRepository = albumRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AlbumDto>> GetAlbumsAsync(string username, AlbumParameters albumParameters)
        {
            var albums = await _albumRepository.GetAlbumsAsync(username, albumParameters);
            return _mapper.Map<IEnumerable<AlbumDto>>(albums);
        }

        public async Task<AlbumDetailsDto> GetAlbumByIdAsync(int albumId)
        {
            var album = await _albumRepository.GetAlbumAsync(albumId);
            return _mapper.Map<AlbumDetailsDto>(album);
        }

        public async Task<AlbumDto> AddAlbumAsync(string username, CreateAlbumDto newAlbum)
        {
            var album = _mapper.Map<Album>(newAlbum);
            await _albumRepository.AddAlbumAsync(username, album);
            return _mapper.Map<AlbumDto>(album);
        }

        public async Task UpdateAlbumAsync(int albumId, UpdateAlbumDto updateAlbum)
        {
            var existingAlbum = await _albumRepository.GetAlbumAsync(albumId);
            var album = _mapper.Map(updateAlbum, existingAlbum);
            await _albumRepository.UpdateAlbumAsync(albumId, album);
        }

        public async Task DeleteAlbumAsync(int albumId)
        {
            var album = await _albumRepository.GetAlbumAsync(albumId);
            await _albumRepository.DeleteAlbumAsync(album);
        }

        public async Task<(int, int, int, bool, bool)> GetMetadata(string username, AlbumParameters albumParameters)
        {
            var albums = await _albumRepository.GetAlbumsAsync(username, albumParameters);
            return (albums.TotalCount, albums.PageSize, albums.CurrentPage, albums.HasNext, albums.HasPrevious);
        }
    }
}
