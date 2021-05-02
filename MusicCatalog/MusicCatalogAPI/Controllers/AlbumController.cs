using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicCatalogAPI.Authorization;
using MusicCatalogAPI.Filters;
using MusicCatalogAPI.Interfaces;
using MusicCatalogAPI.Models.AlbumDtos;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        private readonly IAuthorizationService _authorizationService;

        public AlbumController(IAlbumService albumService, IAuthorizationService authorizationService)
        {
            _albumService = albumService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [Authorize(Roles = "MusicSupplier")]
        public async Task<ActionResult<List<AlbumDto>>> Get([FromQuery] AlbumParameters albumParameters)
        {
            var username = User.FindFirst(c => c.Type == ClaimTypes.Name).Value;
            var albums = await _albumService.GetAlbumsAsync(username, albumParameters);

            var metadata = await _albumService.GetMetadata(username, albumParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(albums);
        }

        [HttpGet("{albumId}")]
        [Authorize(Roles = "MusicSupplier")]
        [ValidateAlbumExistence]
        public async Task<ActionResult<AlbumDetailsDto>> Get(int albumId)
        {
            var album = await _albumService.GetAlbumByIdAsync(albumId);

            var authorizationResult = _authorizationService.AuthorizeAsync(User, album, new ResourceOperationRequirement(OperationType.Read)).Result;
            if (!authorizationResult.Succeeded)
                return Forbid();

            return Ok(album);
        }

        [HttpPost]
        [Authorize(Roles = "MusicSupplier")]
        public async Task<ActionResult> Post([FromBody] CreateAlbumDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = User.FindFirst(c => c.Type == ClaimTypes.Name).Value;

            var album = await _albumService.AddAlbumAsync(username, model);
            return Created("api/album/" + album.Id, null);
        }

        [HttpPut("{albumId}")]
        [ValidateAlbumExistence]
        public async Task<ActionResult> Put(int albumId, [FromBody] UpdateAlbumDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var album = await _albumService.GetAlbumByIdAsync(albumId);

            var authorizationResult = _authorizationService.AuthorizeAsync(User, album, new ResourceOperationRequirement(OperationType.Update)).Result;
            if (!authorizationResult.Succeeded)
                return Forbid();

            await _albumService.UpdateAlbumAsync(albumId, model);
            return NoContent(); 
        }


        [HttpDelete("{albumId}")]
        [ValidateAlbumExistence]
        public async Task<ActionResult> Delete(int albumId)
        {
            var album = await _albumService.GetAlbumByIdAsync(albumId);

            var authorizationResult = _authorizationService.AuthorizeAsync(User, album, new ResourceOperationRequirement(OperationType.Delete)).Result;
            if (!authorizationResult.Succeeded)
                return Forbid();

            await _albumService.DeleteAlbumAsync(albumId);
            return NoContent();
        }
    }
}
