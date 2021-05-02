using AutoMapper;
using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Models.SongDtos;
using MusicCatalogAPI.Repositories.Interfaces;
using MusicCatalogAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Services
{
    public class SongService : ISongService
    {
        private readonly ISongRepository _songRepository;
        private readonly IMapper _mapper;

        public SongService(ISongRepository songRepository, IMapper mapper)
        {
            _songRepository = songRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SongDto>> GetSongsAsync(int albumId)
        {
            var songs = await _songRepository.GetSongsAsync(albumId);
            return _mapper.Map<IEnumerable<SongDto>>(songs);
        }

        public async Task<SongDto> GetSongByIdAsync(int albumId, int songId)
        {
            var song = await _songRepository.GetSongAsync(albumId, songId);
            return _mapper.Map<SongDto>(song);
        }

        public async Task<SongDto> AddSongAsync(int albumId, CreateUpdateSongDto newSong)
        {
            var song = _mapper.Map<Song>(newSong);
            await _songRepository.AddSongAsync(albumId, song);
            return _mapper.Map<SongDto>(song);
        }

        public async Task UpdateSongAsync(int songId, CreateUpdateSongDto updateSong)
        {
            var song = _mapper.Map<Song>(updateSong);
            await _songRepository.UpdateSongAsync(songId, song);
        }

        public async Task DeleteSongAsync(int albumId, int songId)
        {
            var song = await _songRepository.GetSongAsync(albumId, songId);
            await _songRepository.DeleteSongAsync(song);
        }

        public async Task DeleteAllSongsAsync(int albumId)
        {
            await _songRepository.DeleteSongsAsync(albumId);
        }
    }
}
