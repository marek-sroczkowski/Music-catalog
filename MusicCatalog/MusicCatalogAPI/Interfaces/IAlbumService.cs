using MusicCatalogAPI.Filters;
using MusicCatalogAPI.Models.AlbumDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Interfaces
{
    public interface IAlbumService
    {
        Task<IEnumerable<AlbumDto>> GetAlbumsAsync(string username, AlbumParameters albumParameters);
        Task<AlbumDetailsDto> GetAlbumByIdAsync(int albumId);
        Task<AlbumDto> AddAlbumAsync(string username, CreateAlbumDto newAlbum);
        Task UpdateAlbumAsync(int albumId, UpdateAlbumDto updateAlbum);
        Task DeleteAlbumAsync(int albumId);
        Task<(int, int, int, bool, bool)> GetMetadata(string username, AlbumParameters albumParameters);
    }
}
