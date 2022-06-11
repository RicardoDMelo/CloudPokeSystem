using PokemonSystem.Learning.Properties;

namespace PokemonSystem.Learning.Domain.PokemonAggregate
{
    public class MaxMovesException : Exception
    {
        public MaxMovesException() : base(Errors.MaxMoves) { }
    }
}
