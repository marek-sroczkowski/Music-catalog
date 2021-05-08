using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicCatalogAPI.Filters;
using MusicCatalogAPI.Models.SongDtos;
using MusicCatalogAPI.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Controllers
{
    [Route("/api/album/{albumId}/song")]
    [Authorize]
    [ValidateAlbumExistence]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ISongService _songService;

        public SongController(ISongService songService)
        {
            _songService = songService;
        }

        [SwaggerOperation(Summary = "Retrieves all songs from an specific album")]
        [HttpGet]
        public async Task<ActionResult<CreateUpdateSongDto>> Get(int albumId)
        {
            var songs = await _songService.GetSongsAsync(albumId);
            return Ok(songs);
        }

        [SwaggerOperation(Summary = "Creates a new song and assigns it to an specific album")]
        [HttpPost]
        public async Task<ActionResult> Post(int albumId, [FromBody] CreateUpdateSongDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var song = await _songService.AddSongAsync(albumId, model);
            return Created($"api/album/{albumId}/song/{song.Id}", null);
        }

        [SwaggerOperation(Summary = "Updates an existing song from an specific album")]
        [HttpPut("{songId}")]
        [ValidateSongExistence]
        public async Task<ActionResult> Put(int albumId, int songId, [FromBody] CreateUpdateSongDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _songService.UpdateSongAsync(albumId, songId, model);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Deletes a specific song")]
        [HttpDelete("{songId}")]
        [ValidateSongExistence]
        public async Task<ActionResult> Delete(int albumId, int songId)
        {
            await _songService.DeleteSongAsync(albumId, songId);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Deletes all songs from a specific album")]
        [HttpDelete]
        public async Task<ActionResult> Delete(int albumId)
        {
            await _songService.DeleteAllSongsAsync(albumId);
            return NoContent();
        }
    }
}
