using MusicCatalogAPI.Filters;
using MusicCatalogAPI.Helpers;
using MusicCatalogAPI.Models.AlbumDtos;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Services.Interfaces
{
    public interface IAlbumService
    {
        Task<PagedList<AlbumDto>> GetAlbumsAsync(string username, AlbumParameters albumParameters);
        Task<AlbumDetailsDto> GetAlbumByIdAsync(int albumId);
        Task<AlbumDto> AddAlbumAsync(string username, CreateAlbumDto newAlbum);
        Task UpdateAlbumAsync(int albumId, UpdateAlbumDto updatedAlbum);
        Task DeleteAlbumAsync(int albumId);
    }
}
