using PokemonSystem.Common.SeedWork;
using PokemonSystem.Common.ValueObjects;
using System;

namespace PokemonSystem.Learning.PokemonAggregate
{
    public class MoveByLevel : Entity
    {
        public MoveByLevel(Level level, Move move)
        {
            Level = level ?? throw new ArgumentNullException(nameof(level));
            Move = move ?? throw new ArgumentNullException(nameof(move));
        }

        public Level Level { get; private set; }
        public Move Move { get; private set; }
    }
}
