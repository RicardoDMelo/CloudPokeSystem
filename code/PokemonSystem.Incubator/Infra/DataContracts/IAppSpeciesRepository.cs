using PokemonSystem.Incubator.Domain.SpeciesAggregate;

namespace PokemonSystem.Incubator.Infra.DataContracts
{
    public interface IAppSpeciesRepository : ISpeciesRepository
    {
        Task<int> GetCountAsync();
    }
}
