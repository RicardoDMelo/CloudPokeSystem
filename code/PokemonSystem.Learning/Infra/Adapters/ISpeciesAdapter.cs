using PokemonSystem.Learning.Domain.SpeciesAggregate;
using PokemonSystem.Learning.Infra.DatabaseDtos;

namespace PokemonSystem.Learning.Infra.Adapters
{
    public interface ISpeciesAdapter
    {
        Species ConvertToModel(SpeciesDynamoDb speciesDynamoDb);
        SpeciesDynamoDb ConvertToDto(Species species);
    }
}
