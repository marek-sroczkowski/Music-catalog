using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicCatalogAPI.Authorization;
using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Filters;
using MusicCatalogAPI.Models;
using MusicCatalogAPI.Models.Album;
using MusicCatalogAPI.Repositories;
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
        private readonly IAlbumRepository albumRepo;
        private readonly IMapper mapper;
        private readonly IAuthorizationService authorizationService;

        public AlbumController(IAlbumRepository albumRepo, IMapper mapper, IAuthorizationService authorizationService)
        {
            this.albumRepo = albumRepo;
            this.mapper = mapper;
            this.authorizationService = authorizationService;
        }

        [HttpGet]
        [Authorize(Roles = "MusicSupplier")]
        public async Task<ActionResult<List<AlbumDto>>> Get([FromQuery] AlbumParameters albumParameters)
        {
            var username = User.FindFirst(c => c.Type == ClaimTypes.Name).Value;
            var albums = await albumRepo.GetAlbumsAsync(username, albumParameters);

            var metadata = new
            {
                albums.TotalCount,
                albums.PageSize,
                albums.CurrentPage,
                albums.HasNext,
                albums.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(mapper.Map<List<AlbumDto>>(albums));
        }

        [HttpGet("{albumId}")]
        [Authorize(Roles = "MusicSupplier")]
        [ValidateAlbumExistence]
        public async Task<ActionResult<AlbumDetailsDto>> Get(int albumId)
        {
            var album = await albumRepo.GetAlbumAsync(albumId);

            var authorizationResult = authorizationService.AuthorizeAsync(User, album, new ResourceOperationRequirement(OperationType.Read)).Result;
            if (!authorizationResult.Succeeded)
                return Forbid();

            var albumDto = mapper.Map<AlbumDetailsDto>(album);
            return Ok(albumDto);
        }

        [HttpPost]
        [Authorize(Roles = "MusicSupplier")]
        public async Task<ActionResult> Post([FromBody] CreateUpdateAlbumDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var album = mapper.Map<Album>(model);
            var username = User.FindFirst(c => c.Type == ClaimTypes.Name).Value;

            await albumRepo.AddAlbumAsync(username, album);
            return Created("api/album/" + album.Id, null);
        }

        [HttpPut("{albumId}")]
        [ValidateAlbumExistence]
        public async Task<ActionResult> Put(int albumId, [FromBody] CreateUpdateAlbumDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var album = await albumRepo.GetAlbumAsync(albumId);

            var authorizationResult = authorizationService.AuthorizeAsync(User, album, new ResourceOperationRequirement(OperationType.Update)).Result;
            if (!authorizationResult.Succeeded)
                return Forbid();

            var updatedAlbum = mapper.Map<Album>(model);

            await albumRepo.UpdateAlbumAsync(albumId, updatedAlbum);
            return NoContent(); 
        }


        [HttpDelete("{albumId}")]
        [ValidateAlbumExistence]
        public async Task<ActionResult> Delete(int albumId)
        {
            var album = await albumRepo.GetAlbumAsync(albumId);

            var authorizationResult = authorizationService.AuthorizeAsync(User, album, new ResourceOperationRequirement(OperationType.Delete)).Result;
            if (!authorizationResult.Succeeded)
                return Forbid();

            await albumRepo.DeleteAlbumAsync(albumId);
            return NoContent();
        }
    }
}
