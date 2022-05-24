using PokemonSystem.Common.ValueObjects;

namespace PokemonSystem.BillsPC.Domain.PokemonAggregate
{
    public class LearntMoves
    {
        public static int MAX_MOVES = 4;
        public LearntMoves(List<Move> moves)
        {
            if (moves.Count > MAX_MOVES)
            {
                throw new MaxMovesException();
            }

            _learntMoves = moves;
        }

        protected List<Move> _learntMoves { get; set; }
        public IReadOnlyCollection<Move> Values { get => _learntMoves.AsReadOnly(); }
        public int Count { get => _learntMoves.Count; }
    }
}
