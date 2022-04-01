using PokemonSystem.Common.SeedWork;
using PokemonSystem.Incubator.Application;
using PokemonSystem.Incubator.Domain;
using PokemonSystem.Incubator.Domain.SpeciesAggregate;
using PokemonSystem.Incubator.Infra;
using PokemonSystem.PokedexInjector;

namespace PokemonSystem.Incubator
{
    public static class StartupExtensions
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddSingleton<ISpeciesAdapter, SpeciesAdapter>();
            services.AddTransient<IIncubatorService, IncubatorService>();
            services.AddTransient<ISpeciesRepository, SpeciesRepository>();
            services.AddTransient<IAppSpeciesRepository, SpeciesRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IApplicationContext , ApplicationContext>();
        }
    }
}
