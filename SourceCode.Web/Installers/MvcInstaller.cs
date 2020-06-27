using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SourceCode.Web.Models;
using SourceCode.Web.Options;
using SourceCode.Web.Validators;

namespace SourceCode.Web.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews()
                .AddFluentValidation();

            services.AddRazorPages();

            var pagingOptions = new PagingOptions();
            configuration.GetSection(nameof(PagingOptions)).Bind(pagingOptions);
            services.AddSingleton(pagingOptions);

            services.AddTransient<IValidator<ClientViewModel>, ClientValidator>();
            
        }
    }
}
