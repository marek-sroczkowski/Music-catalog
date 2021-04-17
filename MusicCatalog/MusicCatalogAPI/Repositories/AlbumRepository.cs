using Microsoft.EntityFrameworkCore;
using MusicCatalogAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly AppDbContext dbContext;
        private readonly IUserRepository userRepo;
        private readonly IArtistRepository artistRepo;

        public AlbumRepository(AppDbContext dbContext, IUserRepository userRepo, IArtistRepository artistRepo)
        {
            this.dbContext = dbContext;
            this.userRepo = userRepo;
            this.artistRepo = artistRepo;
        }

        public async Task<ICollection<Album>> GetAlbumsAsync() => await dbContext.Albums
            .Include(a => a.Supplier)
            .Include(a => a.Songs)
            .Include(a => a.Artist)
            .ToListAsync();

        public async Task<ICollection<Album>> GetAlbumsAsync(string username)
        {
            var supplier = await userRepo.GetSupplierAsync(username);

            return (await GetAlbumsAsync())
            .Where(a => a.Supplier.Equals(supplier))
            .ToList();
        }

        public async Task<Album> GetAlbumAsync(int albumId) => (await GetAlbumsAsync())
            .FirstOrDefault(a => a.Id.Equals(albumId));

        public async Task AddAlbumAsync(string username, Album album)
        {
            var supplier = await userRepo.GetSupplierAsync(username);
            var artist = await artistRepo.GetArtistAsync(album.Artist.Name);

            if (artist != null)
                album.Artist = artist;

            supplier.Albums.Add(album);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAlbumAsync(int albumId, Album updatedAlbum)
        {
            var album = await GetAlbumAsync(albumId);

            updatedAlbum.Id = album.Id;
            updatedAlbum.ArtistId = album.ArtistId;
            updatedAlbum.SupplierId = album.SupplierId;

            dbContext.Entry(album).CurrentValues.SetValues(updatedAlbum);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAlbumAsync(int albumId)
        {
            var album = await GetAlbumAsync(albumId);

            if (album != null)
            {
                dbContext.Albums.Remove(album);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
