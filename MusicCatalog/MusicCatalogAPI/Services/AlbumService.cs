﻿using AutoMapper;
using MusicCatalogAPI.Filters;
using MusicCatalogAPI.Models.AlbumDtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Services.Interfaces;
using MusicCatalogAPI.Repositories.Interfaces;

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

        public class Metdadata
        {
            public int TotalCount { get; set; }
            public int PageSize { get; set; }
            public int CurrentPage { get; set; }
            public int TotalPages { get; set; }
            public bool HasNext { get; set; }
            public bool HasPrevious { get; set; }
        }

        public async Task<Metdadata> GetMetadata(string username, AlbumParameters albumParameters)
        {
            var albums = await _albumRepository.GetAlbumsAsync(username, albumParameters);
            return new Metdadata {
                TotalCount = albums.TotalCount,
                PageSize = albums.PageSize,
                CurrentPage = albums.CurrentPage,
                TotalPages = albums.TotalPages,
                HasNext = albums.HasNext,
                HasPrevious= albums.HasPrevious
            };
        }
    }
}
