using MusicCatalogAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Repositories
{
    public interface IAlbumRepository
    {
        Task AddAlbumAsync(string username, Album album);
        Task DeleteAlbumAsync(int albumId);
        Task<Album> GetAlbumAsync(int albumId);
        Task<ICollection<Album>> GetAlbumsAsync(string username);
        Task UpdateAlbumAsync(int albumId, Album newAlbum);
    }
}