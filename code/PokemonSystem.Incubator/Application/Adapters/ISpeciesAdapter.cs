using PokemonSystem.Incubator.Domain.SpeciesAggregate;
using PokemonSystem.Incubator.Infra.Database;

namespace PokemonSystem.PokedexInjector
{
    public interface ISpeciesAdapter
    {
        Species ConvertToDto(SpeciesDynamoDb speciesDynamoDb);
    }
}
