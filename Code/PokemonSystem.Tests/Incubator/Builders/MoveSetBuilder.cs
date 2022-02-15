using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Incubator.SpeciesAggregate;
using System.Collections.Generic;

namespace PokemonSystem.Tests.Incubator
{
    public class MoveSetBuilder
    {
        private List<MoveByLevel> _moveSet = new List<MoveByLevel>();

        public MoveSetBuilder()
        {
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
            return _moveSet;
        }
    }
}