namespace PokemonSystem.Incubator.Domain.PokemonAggregate
{
    public interface IIncubatorService
    {
        Pokemon GenerateRandomPokemon(string nickname = null);
    }
}
