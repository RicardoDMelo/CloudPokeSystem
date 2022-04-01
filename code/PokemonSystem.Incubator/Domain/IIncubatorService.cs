using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Incubator.Domain.PokemonAggregate;

namespace PokemonSystem.Incubator.Domain
{
    public interface IIncubatorService
    {
        Task<Pokemon> GenerateRandomPokemonAsync(string? nickname, Level? levelToGrow);
    }
}
