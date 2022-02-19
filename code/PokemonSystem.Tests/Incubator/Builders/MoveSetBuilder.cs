using PokemonSystem.Incubator.Domain.SpeciesAggregate;
using PokemonSystem.Tests.ValueObjects;
using System.Collections.Generic;

namespace PokemonSystem.Tests.Incubator.Builders
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
            var moveSet = _moveSet;
            Reset();
            return moveSet;
        }
    }
}