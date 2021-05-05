using MusicCatalogAPI.Filters;
using MusicCatalogAPI.Models.AlbumDtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MusicCatalogAPI.Services.AlbumService;

namespace MusicCatalogAPI.Services.Interfaces
{
    public interface IAlbumService
    {
        Task<IEnumerable<AlbumDto>> GetAlbumsAsync(string username, AlbumParameters albumParameters);
        Task<AlbumDetailsDto> GetAlbumByIdAsync(int albumId);
        Task<AlbumDto> AddAlbumAsync(string username, CreateAlbumDto newAlbum);
        Task UpdateAlbumAsync(int albumId, UpdateAlbumDto updateAlbum);
        Task DeleteAlbumAsync(int albumId);
        Task<Metdadata> GetMetadata(string username, AlbumParameters albumParameters);
    }
}
