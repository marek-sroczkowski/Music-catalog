using WebApi.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Repositories
{
    public interface IArtistRepository
    {
        Task<Artist> GetArtistAsync(int artistId);
        Task<IEnumerable<Artist>> GetArtistsAsync();
    }
}