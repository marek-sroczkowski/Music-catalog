using MusicCatalogAPI.Models.SongDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Services.Interfaces
{
    public interface ISongService
    {
        Task<IEnumerable<SongDto>> GetSongsAsync(int albumId);
        Task<SongDto> GetSongByIdAsync(int albumId, int songId);
        Task<SongDto> AddSongAsync(int albumId, CreateUpdateSongDto song);
        Task UpdateSongAsync(int albumId, int songId, CreateUpdateSongDto updatedSongModel);
        Task DeleteSongAsync(int albumId, int songId);
        Task DeleteAllSongsAsync(int albumId);
    }
}
