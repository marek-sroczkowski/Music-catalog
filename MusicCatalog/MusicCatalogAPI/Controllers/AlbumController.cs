using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IUserRepository userRepo;
        private readonly IAlbumRepository albumRepo;
        private readonly IMapper mapper;

        public AlbumController(IUserRepository userRepo, IAlbumRepository albumRepo, IMapper mapper)
        {
            this.userRepo = userRepo;
            this.albumRepo = albumRepo;
            this.mapper = mapper;
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
            var username = User.FindFirst(c => c.Type == ClaimTypes.Name).Value;

            var album = await albumRepo.GetAlbumAsync(albumId);
            var albumDto = mapper.Map<AlbumDetailsDto>(album);

            return Ok(albumDto);
        }
    }
}
