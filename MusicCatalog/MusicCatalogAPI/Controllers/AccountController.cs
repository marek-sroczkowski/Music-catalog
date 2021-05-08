using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicCatalogAPI.Identity;
using MusicCatalogAPI.Models.AccountDtos;
using MusicCatalogAPI.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtProvider _jwtProvider;

        public AccountController(IUserService userService, IJwtProvider jwtProvider)
        {
            _userService = userService;
            _jwtProvider = jwtProvider;
        }

        [SwaggerOperation(Summary = "Creates a new supplier")]
        [HttpPost("register/supplier")]
        public async Task<ActionResult> RegisterSupplier([FromBody] RegisterSupplierDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _userService.RegisterSupplierAsync(model);
            return Ok();
        }

        [SwaggerOperation(Summary = "Generates a token when logging in successfully")]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody]LoginUserDto model)
        {
            var user = await _userService.GetUser(model);
            if (user == null)
                return BadRequest("Invalid username or password");

            var passwordVerificationResult = await _userService.ValidatePassword(model);
            if (passwordVerificationResult == PasswordVerificationResult.Failed)
                return BadRequest("Invalid username of password!");

            var token = _jwtProvider.GenerateJwtToken(user);
            return Ok(token);
        }
    }
}
