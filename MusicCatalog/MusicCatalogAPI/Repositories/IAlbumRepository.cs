using MusicCatalogAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Repositories
{
    public interface IAlbumRepository
    {
        Task AddAlbum(string username, Album album);
        Task DeleteAlbum(int albumId);
        Task<Album> GetAlbumAsync(int albumId);
        Task<ICollection<Album>> GetAlbumsAsync(string username);
        Task UpdateAlbum(int albumId, Album newAlbum);
    }
}