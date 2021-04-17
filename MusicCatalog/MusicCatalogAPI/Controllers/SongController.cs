using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Filters;
using MusicCatalogAPI.Models;
using MusicCatalogAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Controllers
{
    [Route("/api/album/{albumId}/song")]
    [Authorize]
    [ValidateAlbumExistence]
    public class SongController : ControllerBase
    {
        private readonly ISongRepository songRepo;
        private readonly IMapper mapper;

        public SongController(ISongRepository songRepo, IMapper mapper)
        {
            this.songRepo = songRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<SongDto>> Get(int albumId)
        {
            var songs = await songRepo.GetSongsAsync(albumId);
            var songDtos = mapper.Map<List<SongDto>>(songs);

            return Ok(songDtos);
        }

        [HttpPost]
        public async Task<ActionResult> Post(int albumId, [FromBody] SongDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var song = mapper.Map<Song>(model);
            await songRepo.AddSongAsync(albumId, song);

            return Created($"api/album/{albumId}", null);
        }

        [HttpPut("{songId}")]
        [ValidateSongExistence]
        public async Task<ActionResult> Put(int albumId, int songId, [FromBody] SongDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedSong = mapper.Map<Song>(model);
            await songRepo.UpdateSongAsync(songId, updatedSong);
            return NoContent();
        }

        [HttpDelete("{songId}")]
        [ValidateSongExistence]
        public async Task<ActionResult> Delete(int albumId, int songId)
        {
            await songRepo.DeleteSongAsync(songId);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int albumId)
        {
            await songRepo.DeleteSongsAsync(albumId);
            return NoContent();
        }
    }
}
