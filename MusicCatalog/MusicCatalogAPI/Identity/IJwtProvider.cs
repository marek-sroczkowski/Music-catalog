using MusicCatalogAPI.Entities;

namespace MusicCatalogAPI.Identity
{
    public interface IJwtProvider
    {
        string GenerateJwtToken(User user);
    }
}