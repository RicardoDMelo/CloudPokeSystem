using PokemonSystem.Common.SeedWork.Domain;
using PokemonSystem.Common.ValueObjects;
using System;

namespace PokemonSystem.Incubator.Domain.SpeciesAggregate
{
    public class MoveByLevel : Entity
    {
        public MoveByLevel(Level level, Move move) : base()
        {
            Level = level ?? throw new ArgumentNullException(nameof(level));
            Move = move ?? throw new ArgumentNullException(nameof(move));
        }

        public Level Level { get; private set; }
        public Move Move { get; private set; }
    }
}
