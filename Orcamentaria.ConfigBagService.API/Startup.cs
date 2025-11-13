using MongoDB.Driver;
using Orcamentaria.ConfigBagService.Application.Services;
using Orcamentaria.ConfigBagService.Domain.Mappers;
using Orcamentaria.ConfigBagService.Domain.Repositories;
using Orcamentaria.ConfigBagService.Domain.Services;
using Orcamentaria.ConfigBagService.Infrastructure.Contexts;
using Orcamentaria.ConfigBagService.Infrastructure.Initializers;
using Orcamentaria.ConfigBagService.Infrastructure.Repositories;
using Orcamentaria.Lib.Infrastructure.Configures;

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
            Configuration = new ConfigurationBagInitializer(_serviceName)
                .InitializeAsync(Configuration)
                .GetAwaiter()
                .GetResult();

            services.AddServiceRegistryHosted(Configuration);

            services.ResolveCommonServices(configuration: Configuration,
                serviceName: _serviceName,
                apiVersion: _apiVersion,
                customServices: () =>
            {
                services.AddScoped<IMongoClient>(_ => new MongoClient(Configuration.GetConnectionString("DefaultConnection")));

                services.AddScoped<MongoContext>();

                //Mappers
                services.AddAutoMapper(_ => { },
                    typeof(ConfigurationBagMapper));

                //Repositories
                services.AddScoped<IConfiguirationBagRepository, ConfigurationBagRepository>();
				
				//Services
                services.AddScoped<IConfiguirationBagService, ConfigurationBagService>();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
			=> app.ConfigureCommon(env, _serviceName, _apiVersion);
    }
}
