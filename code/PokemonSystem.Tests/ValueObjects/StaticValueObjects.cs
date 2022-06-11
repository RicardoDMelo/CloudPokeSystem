using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;

namespace PokemonSystem.Tests.ValueObjects
{
    public static class Levels
    {
        public static Level One = new Level(1);
        public static Level Two = new Level(2);
        public static Level Three = new Level(3);
        public static Level Four = new Level(4);
        public static Level Five = new Level(5);
        public static Level Eight = new Level(8);
        public static Level Ten = new Level(10);
        public static Level Twenty = new Level(20);
        public static Level Fifty = new Level(50);
        public static Level Max = new Level(100);
    }

    public static class Moves
    {
        public static Move Tackle = new Move("Tackle", PokemonType.Normal, MoveCategory.Physical, 60, 0.9, 30);
        public static Move TailWhip = new Move("Tail Whip", PokemonType.Normal, MoveCategory.Status, null, 1, 15);
        public static Move Move1 = new Move("Move1", PokemonType.Normal, MoveCategory.Status, null, 1, 15);
        public static Move Move2 = new Move("Move2", PokemonType.Normal, MoveCategory.Status, null, 1, 15);
        public static Move Move3 = new Move("Move3", PokemonType.Normal, MoveCategory.Status, null, 1, 15);
        public static Move Move4 = new Move("Move4", PokemonType.Normal, MoveCategory.Status, null, 1, 15);
        public static Move Move5 = new Move("Move5", PokemonType.Normal, MoveCategory.Status, null, 1, 15);
        public static Move Move6 = new Move("Move6", PokemonType.Normal, MoveCategory.Status, null, 1, 15);
        public static Move Move7 = new Move("Move7", PokemonType.Normal, MoveCategory.Status, null, 1, 15);
        public static Move Move8 = new Move("Move8", PokemonType.Normal, MoveCategory.Status, null, 1, 15);
        public static Move Move9 = new Move("Move9", PokemonType.Normal, MoveCategory.Status, null, 1, 15);
    }
}
