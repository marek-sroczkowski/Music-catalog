using Microsoft.EntityFrameworkCore;
using MusicCatalogAPI.Data;
using MusicCatalogAPI.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly AppDbContext _dbContext;

        public ArtistRepository(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<Artist>> GetArtistsAsync() => await _dbContext.Artists
            .Include(a => a.Albums)
            .ToListAsync();

        public async Task<Artist> GetArtistAsync(int artistId) => (await GetArtistsAsync())
            .FirstOrDefault(a => a.Id.Equals(artistId));
    }
}
