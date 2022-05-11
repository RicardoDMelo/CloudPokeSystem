using PokemonSystem.Common.SeedWork.Domain;
using PokemonSystem.Common.ValueObjects;

namespace PokemonSystem.Learning.Domain.SpeciesAggregate
{
    public class MoveByLevel : Entity
    {
        public MoveByLevel(Level? level, Move move)
        {
            Level = level;
            Move = move ?? throw new ArgumentNullException(nameof(move));
        }

        public Level? Level { get; private set; }
        public Move Move { get; private set; }
    }
}
