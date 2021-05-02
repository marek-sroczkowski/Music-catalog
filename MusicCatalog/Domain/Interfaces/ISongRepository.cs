using WebApi.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Repositories
{
    public interface ISongRepository
    {
        Task AddSongAsync(int albumId, Song song);
        Task DeleteSongAsync(Song song);
        Task DeleteSongsAsync(int albumId);
        Task<Song> GetSongAsync(int albumId, int songId);
        Task<ICollection<Song>> GetSongsAsync(int albumId);
        Task UpdateSongAsync(int songId, Song newSong);
    }
}