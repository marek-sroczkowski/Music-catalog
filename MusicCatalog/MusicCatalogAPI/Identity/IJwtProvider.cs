using MusicCatalogAPI.Models.AccountDtos;

namespace MusicCatalogAPI.Identity
{
    public interface IJwtProvider
    {
        string GenerateJwtToken(UserDto user);
    }
}