using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicCatalogAPI.Repositories;
using MusicCatalogAPI.Repositories.Interfaces;
using MusicCatalogAPI.Services;
using MusicCatalogAPI.Services.Interfaces;
using System.Reflection;

namespace MusicCatalogAPI.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAlbumRepository, AlbumRepository>();
            services.AddTransient<IArtistRepository, ArtistRepository>();
            services.AddTransient<ISongRepository, SongRepository>();

            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<ISongService, SongService>();
            services.AddScoped<IUserService, UserService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
