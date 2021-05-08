using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicCatalogAPI.Authorization;
using MusicCatalogAPI.Filters;
using MusicCatalogAPI.Models.AlbumDtos;
using MusicCatalogAPI.Services.Interfaces;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        private readonly IAuthorizationService _authorizationService;

        public AlbumController(IAlbumService albumService, IAuthorizationService authorizationService)
        {
            _albumService = albumService;
            _authorizationService = authorizationService;
        }

        [SwaggerOperation(Summary = "Retrieves all albums")]
        [HttpGet]
        [Authorize(Roles = "MusicSupplier")]
        public async Task<ActionResult<List<AlbumDto>>> Get([FromQuery] AlbumParameters albumParameters)
        {
            var username = User.FindFirst(c => c.Type == ClaimTypes.Name).Value;
            var albums = await _albumService.GetAlbumsAsync(username, albumParameters);

            var metadata = new
            {
                albums.TotalCount,
                albums.PageSize,
                albums.CurrentPage,
                albums.TotalPages,
                albums.HasNext,
                albums.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(albums);
        }

        [SwaggerOperation(Summary = "Retrieves a specific album by unique id")]
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

        [SwaggerOperation(Summary = "Creates a new album")]
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

        [SwaggerOperation(Summary = "Updates a existing album")]
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

        [SwaggerOperation(Summary = "Deletes a specific album")]
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
