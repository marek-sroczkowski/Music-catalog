using MusicCatalogAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Repositories
{
    public interface IArtistRepository
    {
        Task<Artist> GetArtistAsync(string name);
        Task<ICollection<Artist>> GetArtistsAsync();
    }
}