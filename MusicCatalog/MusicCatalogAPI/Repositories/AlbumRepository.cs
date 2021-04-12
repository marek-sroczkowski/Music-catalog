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

        public AlbumRepository(AppDbContext dbContext, IUserRepository userRepo)
        {
            this.dbContext = dbContext;
            this.userRepo = userRepo;
        }

        public async Task<ICollection<Album>> GetAlbumsAsync(string username)
        {
            var supplier = await userRepo.GetSupplierAsync(username);

            return await dbContext.Albums
            .Include(a => a.Supplier)
            .Include(a => a.Songs)
            .Include(a => a.Artist)
            .Where(a => a.Supplier.Equals(supplier))
            .ToListAsync();
        }

        public async Task<Album> GetAlbumAsync(int albumId) => await dbContext.Albums
            .Include(a => a.Supplier)
            .Include(a => a.Songs)
            .Include(a => a.Artist)
            .FirstOrDefaultAsync(a => a.Id.Equals(albumId));

        public async Task AddAlbum(string username, Album album)
        {
            var supplier = await userRepo.GetSupplierAsync(username);

            supplier?.Albums.Add(album);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAlbum(int albumId, Album newAlbum)
        {

        }

        public async Task DeleteAlbum(int albumId)
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
