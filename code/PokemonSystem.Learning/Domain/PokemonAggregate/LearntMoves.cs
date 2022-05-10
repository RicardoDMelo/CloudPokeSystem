using PokemonSystem.Common.ValueObjects;

namespace PokemonSystem.Learning.Domain.PokemonAggregate
{
    public class LearntMoves
    {
        public static int MAX_MOVES = 4;
        public LearntMoves()
        {
            _learntMoves = new List<Move>();
        }

        protected List<Move> _learntMoves { get; set; }
        public IReadOnlyCollection<Move> Values { get => _learntMoves.AsReadOnly(); }
        public int Count { get => _learntMoves.Count; }

        public void AddMove(Move move)
        {
            if (_learntMoves.Count == MAX_MOVES)
            {
                throw new MaxMovesException();
            }

            _learntMoves.Add(move);
        }

        public void ForgetMove(Move move)
        {
            if (_learntMoves.Contains(move))
            {
                _learntMoves.Remove(move);
            }
        }
    }
}
