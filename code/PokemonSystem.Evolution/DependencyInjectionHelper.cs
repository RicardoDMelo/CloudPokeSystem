using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.SimpleNotificationService;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokemonSystem.Common.SeedWork;
using PokemonSystem.Evolution.Application;
using PokemonSystem.Evolution.Application.Adapters;
using PokemonSystem.Evolution.Application.Commands;
using PokemonSystem.Evolution.Infra;
using PokemonSystem.Evolution.Infra.DataContracts;

namespace PokemonSystem.Evolution
{
    public static class DependencyInjectionHelper
    {
        public static IServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();
            services.ConfigureServices();
            return services.BuildServiceProvider();
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            IConfiguration config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .AddEnvironmentVariables()
               .Build();

            services.AddMediatR(typeof(GrantPokemonLevel));
            services.AddLogging();
            services.AddSingleton(config);

            services.ConfigureAWS(config);
            services.ConfigureDependencyInjection();

            return services;
        }

        public static IServiceCollection ConfigureAWS(this IServiceCollection services, IConfiguration config)
        {
            services.AddDefaultAWSOptions(config.GetAWSOptions());
            services.AddAWSService<IAmazonSimpleNotificationService>();
            services.AddAWSService<IAmazonDynamoDB>();
            services.AddSingleton<IDynamoDBContext, DynamoDBContext>();

            return services;
        }

        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddSingleton<IPokemonAdapter, PokemonAdapter>();
            services.AddTransient<ISpeciesRepository, SpeciesRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IApplicationContext, ApplicationContext>();

            return services;
        }
    }
}
