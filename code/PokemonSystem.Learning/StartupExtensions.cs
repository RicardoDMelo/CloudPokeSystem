using PokemonSystem.Common.SeedWork;

namespace PokemonSystem.Learning
{
    public static class StartupExtensions
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IApplicationContext, ApplicationContext>();
        }
    }
}
