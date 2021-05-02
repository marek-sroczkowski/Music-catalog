using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Filters;
using MusicCatalogAPI.Helpers;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Repositories
{
    public interface IAlbumRepository
    {
        Task AddAlbumAsync(string username, Album album);
        Task DeleteAlbumAsync(Album album);
        Task<Album> GetAlbumAsync(int albumId);
        Task<PagedList<Album>> GetAlbumsAsync(string username, AlbumParameters albumParameters);
        Task UpdateAlbumAsync(int albumId, Album newAlbum);
    }
}