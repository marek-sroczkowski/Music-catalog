using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicCatalogAPI.Models;
using MusicCatalogAPI.Repositories;
using MusicCatalogAPI.Validators;

namespace MusicCatalogAPI.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAlbumRepository, AlbumRepository>();
            services.AddTransient<IArtistRepository, ArtistRepository>();
            services.AddTransient<ISongRepository, SongRepository>();
            services.AddAutoMapper(GetType().Assembly);
            services.AddControllers().AddFluentValidation();
            services.AddScoped<IValidator<RegisterSupplierDto>, RegisterSupplierValidation>();
        }
    }
}
