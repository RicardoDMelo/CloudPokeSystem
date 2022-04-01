using PokemonSystem.Incubator.Infra.Database;

namespace PokemonSystem.Incubator.Domain.SpeciesAggregate
{
    public interface ISpeciesRepository
    {
        Task<SpeciesDynamoDb> GetRandomSpeciesAsync();
    }
}
