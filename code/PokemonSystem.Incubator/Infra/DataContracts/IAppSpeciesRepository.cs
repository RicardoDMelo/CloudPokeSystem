using PokemonSystem.Incubator.Infra.Database;

namespace PokemonSystem.Incubator.Domain.SpeciesAggregate
{
    public interface IAppSpeciesRepository : ISpeciesRepository
    {
        Task<int> GetCountAsync();
    }
}
