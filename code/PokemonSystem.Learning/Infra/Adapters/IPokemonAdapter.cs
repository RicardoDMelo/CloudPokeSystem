using PokemonSystem.Learning.Domain.PokemonAggregate;
using PokemonSystem.Learning.Infra.DatabaseDtos;

namespace PokemonSystem.Learning.Infra.Adapters
{
    public interface IPokemonAdapter
    {
        Pokemon ConvertToModel(PokemonDynamoDb pokemonDynamoDb);
        PokemonDynamoDb ConvertToDto(Pokemon pokemon);
    }
}
