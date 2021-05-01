using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicCatalogAPI.Models.AccountDtos;
using MusicCatalogAPI.Repositories;
using MusicCatalogAPI.Validators;
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
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddControllers().AddFluentValidation();
            services.AddScoped<IValidator<RegisterSupplierDto>, RegisterSupplierValidation>();
        }
    }
}
