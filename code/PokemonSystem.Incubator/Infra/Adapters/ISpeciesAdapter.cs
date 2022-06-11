using PokemonSystem.Incubator.Domain.SpeciesAggregate;
using PokemonSystem.Incubator.Infra.DatabaseDtos;

namespace PokemonSystem.Incubator.Infra.Adapters
{
    public interface ISpeciesAdapter
    {
        Species ConvertToModel(SpeciesDynamoDb speciesDynamoDb);
    }
}
