using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Driver;
using Orcamentaria.ConfigBagService.Application.Services;
using Orcamentaria.ConfigBagService.Domain.Repositories;
using Orcamentaria.ConfigBagService.Domain.Services;
using Orcamentaria.ConfigBagService.Infrastructure.Contexts;
using Orcamentaria.ConfigBagService.Infrastructure.Repositories;
using Orcamentaria.Lib.Infrastructure;
using Orcamentaria.PersonService.Domain.Mappers;

namespace Orcamentaria.ConfigBagService.API
{
    public class Startup
    {
        private readonly string _serviceName = "Orcamentaria.ConfigBagService";
        private readonly string _apiVersion = "v1";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            Configuration = new Infrastructure.Initializers.ConfigurationBagInitializer(_serviceName)
                .InitializeAsync(Configuration)
                .GetAwaiter()
                .GetResult();

            services.Replace(ServiceDescriptor.Singleton(Configuration));

            CommonDI.AddServiceRegistryHosted(services, Configuration);
            
            CommonDI.ResolveCommonServices(_serviceName, _apiVersion, services, Configuration, () =>
            {
                services.AddScoped<IMongoClient>(_ => new MongoClient(Configuration.GetConnectionString("DefaultConnection")));

                services.AddScoped<MongoContext>();

                //Mappers
                services.AddAutoMapper(
                    typeof(ConfigurationBagMapper));

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
