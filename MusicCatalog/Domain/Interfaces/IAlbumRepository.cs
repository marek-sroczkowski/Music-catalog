using WebApi.Entities;
using WebApi.Filters;
using WebApi.Helpers;
using System.Threading.Tasks;

namespace WebApi.Repositories
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