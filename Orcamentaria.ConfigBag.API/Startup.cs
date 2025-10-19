using Orcamentaria.ConfigBag.Application.Services;
using Orcamentaria.ConfigBag.Domain.Repositories;
using Orcamentaria.ConfigBag.Domain.Services;
using Orcamentaria.ConfigBag.Infrastructure.Contexts;
using Orcamentaria.ConfigBag.Infrastructure.Repositories;
using Orcamentaria.Lib.Infrastructure;

namespace Orcamentaria.ConfigBag.API
{
    public class Startup
    {
        private readonly string _serviceName = "Orcamentaria.ConfigBag";
        private readonly string _apiVersion = "v1";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            CommonDI.AddServiceRegistryHosted(services, Configuration);
            
            CommonDI.ResolveCommonServices(_serviceName, _apiVersion, services, Configuration, () =>
            {

                services.AddScoped<MongoContext>();

                //Repositories
                services.AddScoped<IConfiguirationBagRepository, ConfigurationBagRepository>();
				
				//Services
                services.AddScoped<IConfiguirationBagService, ConfigurationBagService>();
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
			=> CommonDI.ConfigureCommon(_serviceName, _apiVersion, app, env);
    }
}
