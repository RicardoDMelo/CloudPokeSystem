using PokemonSystem.Evolution.Application.Commands;
using PokemonSystem.Evolution.Domain.PokemonAggregate;
using PokemonSystem.Evolution.Infra.DatabaseDtos;

namespace PokemonSystem.Evolution.Application.Adapters
{
    public interface IPokemonAdapter
    {
        Pokemon ConvertToModel(GrantPokemonLevel grantPokemonLevel, SpeciesDynamoDb species);
    }
}
