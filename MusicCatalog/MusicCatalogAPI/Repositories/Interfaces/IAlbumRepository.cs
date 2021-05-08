using MusicCatalogAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Repositories.Interfaces
{
    public interface IAlbumRepository
    {
        Task AddAlbumAsync(string username, Album album);
        Task DeleteAlbumAsync(Album album);
        Task<Album> GetAlbumAsync(int albumId);
        Task<IEnumerable<Album>> GetAlbumsAsync(string username);
        Task UpdateAlbumAsync(Album newAlbum);
    }
}