using PokemonSystem.BillsPC.Properties;

namespace PokemonSystem.BillsPC.Domain.PokemonAggregate
{
    public class MaxMovesException : Exception
    {
        public MaxMovesException() : base(Errors.MaxMoves) { }
    }
}
