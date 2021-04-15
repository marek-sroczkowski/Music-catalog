using Microsoft.EntityFrameworkCore;
using MusicCatalogAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly AppDbContext dbContext;

        public ArtistRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ICollection<Artist>> GetArtistsAsync() => await dbContext.Artists
            .Include(a => a.Albums)
            .ToListAsync();

        public async Task<Artist> GetArtistAsync(string name) => await dbContext.Artists
            .Include(a => a.Albums)
            .FirstOrDefaultAsync(a => a.Name.Equals(name));
    }
}
