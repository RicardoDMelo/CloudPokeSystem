using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.SimpleNotificationService;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokemonSystem.Common.SeedWork;
using PokemonSystem.Incubator.Application;
using PokemonSystem.Incubator.Application.Adapters;
using PokemonSystem.Incubator.Application.Commands;
using PokemonSystem.Incubator.Domain;
using PokemonSystem.Incubator.Domain.SpeciesAggregate;
using PokemonSystem.Incubator.Infra;
using PokemonSystem.Incubator.Infra.DataContracts;

namespace PokemonSystem.Incubator
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

            services.AddMediatR(typeof(CreateRandomPokemon));
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
            services.AddSingleton<ISpeciesAdapter, SpeciesAdapter>();
            services.AddTransient<IIncubatorService, IncubatorService>();
            services.AddTransient<ISpeciesRepository, SpeciesRepository>();
            services.AddTransient<IAppSpeciesRepository, SpeciesRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IApplicationContext, ApplicationContext>();

            return services;
        }
    }
}
