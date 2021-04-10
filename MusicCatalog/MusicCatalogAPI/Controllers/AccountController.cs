using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Identity;
using MusicCatalogAPI.Models;
using MusicCatalogAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository userRepo;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly IJwtProvider jwtProvider;
        private readonly IMapper mapper;

        public AccountController(IUserRepository userRepo, IPasswordHasher<User> passwordHasher, IJwtProvider jwtProvider, IMapper mapper)
        {
            this.userRepo = userRepo;
            this.passwordHasher = passwordHasher;
            this.jwtProvider = jwtProvider;
            this.mapper = mapper;
        }

        [HttpPost("register/supplier")]
        public ActionResult RegisterSupplier([FromBody] RegisterSupplierDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newUser = mapper.Map<Supplier>(model);
            newUser.PasswordHash = passwordHasher.HashPassword(newUser, model.Password);

            userRepo.AddUserAsync(newUser);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody]LoginUserDto model)
        {
            var user = userRepo.GetUserAsync(model.Username);

            if (user == null)
                return BadRequest("Invalid username or password");

            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user.Result, user.Result.PasswordHash, model.Password);
            if (passwordVerificationResult == PasswordVerificationResult.Failed)
                return BadRequest("Invalid username of password!");

            var token = jwtProvider.GenerateJwtToken(user.Result);
            return Ok(token);
        }
    }
}
