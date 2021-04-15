﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicCatalogAPI.Authorization;
using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Models;
using MusicCatalogAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ActionResult<List<AlbumDto>>> Get()
        {
            var username = User.FindFirst(c => c.Type == ClaimTypes.Name).Value;

            var albums = await albumRepo.GetAlbumsAsync(username);
            var albumDtos = mapper.Map<List<AlbumDto>>(albums);

            return Ok(albumDtos);
        }

        [HttpGet("{albumId}")]
        [Authorize(Roles = "MusicSupplier")]
        public async Task<ActionResult<AlbumDto>> Get(int albumId)
        {
            var album = await albumRepo.GetAlbumAsync(albumId);
            if (album == null)
                return NotFound();

            var authorizationResult = authorizationService.AuthorizeAsync(User, album, new ResourceOperationRequirement(OperationType.Read)).Result;
            if (!authorizationResult.Succeeded)
                return Forbid();

            var albumDto = mapper.Map<AlbumDetailsDto>(album);

            return Ok(albumDto);
        }

        [HttpPost]
        [Authorize(Roles = "MusicSupplier")]
        public async Task<ActionResult> Post([FromBody] AlbumDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var album = mapper.Map<Album>(model);
            var username = User.FindFirst(c => c.Type == ClaimTypes.Name).Value;

            await albumRepo.AddAlbumAsync(username, album);

            return Created("api/album/" + album.Id, null);
        }

        [HttpPut("{albumId}")]
        public async Task<ActionResult> Put(int albumId, [FromBody] AlbumDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var album = await albumRepo.GetAlbumAsync(albumId);
            if (album == null)
                return NotFound();

            var authorizationResult = authorizationService.AuthorizeAsync(User, album, new ResourceOperationRequirement(OperationType.Update)).Result;
            if (!authorizationResult.Succeeded)
                return Forbid();

            var updatedAlbum = mapper.Map<Album>(model);

            await albumRepo.UpdateAlbumAsync(albumId, updatedAlbum);
            return NoContent(); 
        }


        [HttpDelete("{albumId}")]
        public async Task<ActionResult> Delete(int albumId)
        {
            var album = await albumRepo.GetAlbumAsync(albumId);
            if (album == null)
                return NotFound();

            var authorizationResult = authorizationService.AuthorizeAsync(User, album, new ResourceOperationRequirement(OperationType.Delete)).Result;
            if (!authorizationResult.Succeeded)
                return Forbid();

            await albumRepo.DeleteAlbumAsync(albumId);
            return NoContent();
        }
    }
}
