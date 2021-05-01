using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Filters;
using MusicCatalogAPI.Helpers;
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
        Task<PagedList<Album>> GetAlbumsAsync(string username, AlbumParameters albumParameters);
        Task UpdateAlbumAsync(int albumId, Album newAlbum);
    }
}