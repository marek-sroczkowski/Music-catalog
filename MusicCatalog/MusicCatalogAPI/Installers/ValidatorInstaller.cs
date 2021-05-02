using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicCatalogAPI.Models.AccountDtos;
using MusicCatalogAPI.Validators;

namespace MusicCatalogAPI.Installers
{
    public class ValidatorInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers().AddFluentValidation();
            services.AddScoped<IValidator<RegisterSupplierDto>, RegisterSupplierValidation>();
        }
    }
}
