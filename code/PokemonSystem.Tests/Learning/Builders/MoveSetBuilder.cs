using PokemonSystem.Learning.Domain.PokemonAggregate;
using PokemonSystem.Tests.ValueObjects;

namespace PokemonSystem.Tests.Learning.Builders
{
    public class MoveSetBuilder
    {
        private List<MoveByLevel> _moveSet = new List<MoveByLevel>();

        public MoveSetBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            _moveSet.Clear();
            _moveSet.Add(new MoveByLevel(Levels.One, Moves.Tackle));
            _moveSet.Add(new MoveByLevel(Levels.Two, Moves.TailWhip));
            _moveSet.Add(new MoveByLevel(Levels.Three, Moves.Move1));
            _moveSet.Add(new MoveByLevel(Levels.Four, Moves.Move2));
            _moveSet.Add(new MoveByLevel(Levels.Five, Moves.Move3));
            _moveSet.Add(new MoveByLevel(Levels.Ten, Moves.Move4));
            _moveSet.Add(new MoveByLevel(Levels.Ten, Moves.Move5));
            _moveSet.Add(new MoveByLevel(Levels.Twenty, Moves.Move6));
            _moveSet.Add(new MoveByLevel(Levels.Fifty, Moves.Move7));
            _moveSet.Add(new MoveByLevel(Levels.Max, Moves.Move8));
            _moveSet.Add(new MoveByLevel(null, Moves.Move9));
        }

        public MoveSetBuilder AddMove(MoveByLevel moveByLevel)
        {
            _moveSet.Add(moveByLevel);
            return this;
        }

        public MoveSetBuilder ResetMoves()
        {
            _moveSet.Clear();
            return this;
        }

        public List<MoveByLevel> Build()
        {
            var moveSet = _moveSet.ToList();
            Reset();
            return moveSet;
        }
    }
}