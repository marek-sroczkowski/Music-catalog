using AutoMapper;
using MusicCatalogAPI.Filters;
using MusicCatalogAPI.Models.AlbumDtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Services.Interfaces;
using MusicCatalogAPI.Repositories.Interfaces;
using System.Linq;
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

        public async Task<PagedList<AlbumDto>> GetAlbumsAsync(string username, AlbumParameters albumParameters)
        {
            var albums = await _albumRepository.GetAlbumsAsync(username);

            SearchByTitle(ref albums, albumParameters.Title);
            SearchByArtistName(ref albums, albumParameters.ArtistName);
            SearchByPublicationYear(ref albums, albumParameters.PublicationYear);

            var albumsDto = _mapper.Map<ICollection<AlbumDto>>(albums);
            return PagedList<AlbumDto>.ToPagedList(albumsDto.ToList(), albumParameters.PageNumber, albumParameters.PageSize);
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

        public async Task UpdateAlbumAsync(int albumId, UpdateAlbumDto updatedAlbum)
        {
            var existingAlbum = await _albumRepository.GetAlbumAsync(albumId);
            var album = _mapper.Map(updatedAlbum, existingAlbum);
            await _albumRepository.UpdateAlbumAsync(album);
        }

        public async Task DeleteAlbumAsync(int albumId)
        {
            var album = await _albumRepository.GetAlbumAsync(albumId);
            await _albumRepository.DeleteAlbumAsync(album);
        }

        private void SearchByArtistName(ref IEnumerable<Album> albums, string artistName)
        {
            if (string.IsNullOrEmpty(artistName))
                return;

            albums = albums.Where(a => a.Artist.Name.ToLowerInvariant().Contains(artistName.ToLowerInvariant()));
        }

        private void SearchByTitle(ref IEnumerable<Album> albums, string title)
        {
            if (string.IsNullOrEmpty(title))
                return;

            albums = albums.Where(a => a.Title.ToLowerInvariant().Contains(title.ToLowerInvariant()));
        }

        private void SearchByPublicationYear(ref IEnumerable<Album> albums, int publicationYear)
        {
            if (publicationYear == 0)
                return;

            albums = albums.Where(a => a.PublicationYear.Equals(publicationYear));
        }
    }
}
