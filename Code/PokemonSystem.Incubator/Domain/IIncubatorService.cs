using PokemonSystem.Incubator.SpeciesAggregate;

namespace PokemonSystem.Incubator.PokemonAggregate
{
    public interface IIncubatorService
    {
        Pokemon GenerateRandomPokemon(string nickname = null);
    }
}
