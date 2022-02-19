using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;

namespace PokemonSystem.Tests.ValueObjects
{
    public static class Levels
    {
        public static Level One = new Level(1);
        public static Level Two = new Level(2);
        public static Level Ten = new Level(10);
        public static Level Fifty = new Level(50);
        public static Level Max = new Level(100);
    }

    public static class Moves
    {
        public static Move Tackle = new Move("Tackle", PokemonType.Normal, MoveCategory.Physical, 60, 0.9, 30);
        public static Move TailWhip = new Move("Tail Whip", PokemonType.Normal, MoveCategory.Status, null, 1, 15);
    }
}
