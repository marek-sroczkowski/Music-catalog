using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicCatalogAPI.Filters;
using MusicCatalogAPI.Models.SongDtos;
using MusicCatalogAPI.Services.Interfaces;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Controllers
{
    [Route("/api/album/{albumId}/song")]
    [Authorize]
    [ValidateAlbumExistence]
    public class SongController : ControllerBase
    {
        private readonly ISongService _songService;

        public SongController(ISongService songService)
        {
            _songService = songService;
        }

        [HttpGet]
        public async Task<ActionResult<CreateUpdateSongDto>> Get(int albumId)
        {
            var songs = await _songService.GetSongsAsync(albumId);
            return Ok(songs);
        }

        [HttpPost]
        public async Task<ActionResult> Post(int albumId, [FromBody] CreateUpdateSongDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var song = await _songService.AddSongAsync(albumId, model);
            return Created($"api/album/{albumId}/song/{song.Id}", null);
        }

        [HttpPut("{songId}")]
        [ValidateSongExistence]
        public async Task<ActionResult> Put(int albumId, int songId, [FromBody] CreateUpdateSongDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _songService.UpdateSongAsync(songId, model);
            return NoContent();
        }

        [HttpDelete("{songId}")]
        [ValidateSongExistence]
        public async Task<ActionResult> Delete(int albumId, int songId)
        {
            await _songService.DeleteSongAsync(albumId, songId);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int albumId)
        {
            await _songService.DeleteAllSongsAsync(albumId);
            return NoContent();
        }
    }
}
