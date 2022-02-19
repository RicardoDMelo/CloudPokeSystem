using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Incubator.Domain.SpeciesAggregate;
using System.Collections.Generic;

namespace PokemonSystem.Incubator.Infra
{
    internal class SpeciesRepository : ISpeciesRepository
    {
        public Species GetRandomSpecies()
        {
            var moveset = new List<MoveByLevel>() {
                new MoveByLevel(new Level(1), new Move("Tackle", PokemonType.Normal, MoveCategory.Physical, 60, 1, 30)),
                new MoveByLevel(new Level(2), new Move("TailWhip", PokemonType.Normal, MoveCategory.Status, null, 1, 30)),
            };

            return new Species(
                100,
                "Tauros",
                new Typing(PokemonType.Normal),
                new Stats(1, 1, 1, 1, 1, 1),
                0.5,
                null,
                moveset);
        }
    }
}
