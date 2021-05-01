using Microsoft.EntityFrameworkCore;
using MusicCatalogAPI.Data;
using MusicCatalogAPI.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Repositories
{
    public class SongRepository : ISongRepository
    {
        private readonly AppDbContext dbContext;
        private readonly IAlbumRepository albumRepo;

        public SongRepository(AppDbContext dbContext, IAlbumRepository albumRepo)
        {
            this.dbContext = dbContext;
            this.albumRepo = albumRepo;
        }

        public async Task<ICollection<Song>> GetSongsAsync(int albumId) => await dbContext.Songs
            .Include(s => s.Album)
            .Where(s => s.AlbumId.Equals(albumId))
            .ToListAsync();

        public async Task<Song> GetSongAsync(int albumId, int songId) => (await GetSongsAsync(albumId))
            .FirstOrDefault(s => s.Id.Equals(songId));

        public async Task AddSongAsync(int albumId, Song song)
        {
            var album = await albumRepo.GetAlbumAsync(albumId);

            album.Songs.Add(song);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateSongAsync(int songId, Song newSong)
        {
            var song = await dbContext.Songs
                .Include(s => s.Album)
                .FirstOrDefaultAsync(s => s.Id.Equals(songId));

            newSong.Id = song.Id;
            newSong.AlbumId = song.AlbumId;

            dbContext.Entry(song).CurrentValues.SetValues(newSong);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteSongAsync(int songId)
        {
            var song = await dbContext.Songs
            .Include(s => s.Album)
            .FirstOrDefaultAsync(s => s.Id.Equals(songId));

            dbContext.Remove(song);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteSongsAsync(int albumId)
        {
            var album = await albumRepo.GetAlbumAsync(albumId);
            dbContext.Songs.RemoveRange(album.Songs);
            await dbContext.SaveChangesAsync();
        }
    }
}
