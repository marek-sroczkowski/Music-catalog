using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MusicCatalogAPI.Installers
{
    public class CorsInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AngularClient", builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));
            });
        }
    }
}
