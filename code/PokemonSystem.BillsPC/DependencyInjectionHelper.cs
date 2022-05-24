using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.SimpleNotificationService;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PokemonSystem.BillsPC;
using PokemonSystem.BillsPC.Application;
using PokemonSystem.BillsPC.Application.Commands;
using PokemonSystem.BillsPC.Application.Handlers;
using PokemonSystem.BillsPC.Domain;
using PokemonSystem.BillsPC.Domain.PokemonAggregate;
using PokemonSystem.BillsPC.Domain.SpeciesAggregate;
using PokemonSystem.BillsPC.Infra;
using PokemonSystem.BillsPC.Infra.Adapters;
using PokemonSystem.Common.SeedWork;

namespace PokemonSystem.BillsPC
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
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            services.AddMediatR(typeof(DependencyInjectionHelper));
            services.AddLogging(opt =>
            {
                opt.AddLambdaLogger();
                opt.SetMinimumLevel(LogLevel.Information);
            });
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IApplicationContext, ApplicationContext>();

            services.AddScoped<ISpeciesRepository, SpeciesRepository>();
            services.AddScoped<IPokemonRepository, PokemonRepository>();
            services.AddScoped<IPokemonAdapter, PokemonAdapter>();
            services.AddScoped<ISpeciesAdapter, SpeciesAdapter>();

            return services;
        }
    }
}
