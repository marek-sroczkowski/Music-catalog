using MusicCatalogAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Repositories.Interfaces
{
    public interface IArtistRepository
    {
        Task<Artist> GetArtistAsync(string name);
        Task<IEnumerable<Artist>> GetArtistsAsync();
    }
}