using Microsoft.EntityFrameworkCore;
using MusicCatalogAPI.Data;
using MusicCatalogAPI.Entities;
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

        public async Task UpdateAlbumAsync(Album updatedAlbum)
        {
            _dbContext.Update(updatedAlbum);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAlbumAsync(Album album)
        {
            _dbContext.Albums.Remove(album);
            await _dbContext.SaveChangesAsync();
        }
    }
}
