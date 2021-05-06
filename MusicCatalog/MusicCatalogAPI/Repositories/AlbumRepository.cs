using Microsoft.EntityFrameworkCore;
using MusicCatalogAPI.Data;
using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Filters;
using MusicCatalogAPI.Helpers;
using MusicCatalogAPI.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IUserRepository _userRepository;
        private readonly IArtistRepository _artistRepository;

        public AlbumRepository(AppDbContext dbContext, IUserRepository userRepo, IArtistRepository artistRepo)
        {
            _dbContext = dbContext;
            _userRepository = userRepo;
            _artistRepository = artistRepo;
        }

        private async Task<IEnumerable<Album>> GetAllAlbumsAsync() => await _dbContext.Albums
            .Include(a => a.Supplier)
            .Include(a => a.Songs)
            .Include(a => a.Artist)
            .ToListAsync();

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

        private void SetIds(Album existingAlbum, Album updatedAlbum)
        {
            updatedAlbum.Id = existingAlbum.Id;
            updatedAlbum.ArtistId = existingAlbum.ArtistId;
            updatedAlbum.SupplierId = existingAlbum.SupplierId;
        }


        public async Task<PagedList<Album>> GetAlbumsAsync(string username, AlbumParameters albumParameters)
        {
            var albums = await GetAllAlbumsAsync();
            SearchByArtistName(ref albums, albumParameters.ArtistName);
            SearchByTitle(ref albums, albumParameters.Title);
            SearchByPublicationYear(ref albums, albumParameters.PublicationYear);

            return PagedList<Album>.ToPagedList(albums.ToList(), albumParameters.PageNumber, albumParameters.PageSize);
        }

        public async Task<IEnumerable<Album>> GetAlbumsAsync(string username)
        {
            var supplier = await _userRepository.GetSupplierAsync(username);

            return (await GetAllAlbumsAsync())
            .Where(a => a.Supplier.Equals(supplier))
            .ToList();
        }

        public async Task<Album> GetAlbumAsync(int albumId) => (await GetAllAlbumsAsync())
            .FirstOrDefault(a => a.Id.Equals(albumId));

        public async Task AddAlbumAsync(string username, Album album)
        {
            var supplier = await _userRepository.GetSupplierAsync(username);
            var artist = await _artistRepository.GetArtistAsync(album.Artist.Name);

            if (artist != null)
                album.Artist = artist;

            supplier.Albums.Add(album);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAlbumAsync(int albumId, Album updatedAlbum)
        {
            var existingAlbum = await GetAlbumAsync(albumId);
            SetIds(existingAlbum, updatedAlbum);

            _dbContext.Entry(existingAlbum).CurrentValues.SetValues(updatedAlbum);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAlbumAsync(Album album)
        {
            _dbContext.Albums.Remove(album);
            await _dbContext.SaveChangesAsync();
        }
    }
}
