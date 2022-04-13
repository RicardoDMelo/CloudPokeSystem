using PokemonSystem.Common.SeedWork;
using PokemonSystem.Evolution.Application;
using PokemonSystem.Evolution.Application.Adapters;
using PokemonSystem.Evolution.Infra;
using PokemonSystem.Evolution.Infra.DataContracts;

namespace PokemonSystem.Evolution
{
    public static class StartupExtensions
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddSingleton<IPokemonAdapter, PokemonAdapter>();
            services.AddTransient<ISpeciesRepository, SpeciesRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IApplicationContext, ApplicationContext>();
        }
    }
}
