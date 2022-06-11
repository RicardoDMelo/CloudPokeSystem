using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Learning.Domain.PokemonAggregate;

namespace PokemonSystem.Learning.Domain
{
    public interface ILearningService
    {
        Task<Pokemon> TeachRandomMovesAsync(Guid pokemonId, Level level, uint speciesId);
    }
}
