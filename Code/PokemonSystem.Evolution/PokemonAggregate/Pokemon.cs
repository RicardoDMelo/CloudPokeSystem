using PokemonSystem.Common.SeedWork;
using PokemonSystem.Common.ValueObjects;
using System;

namespace PokemonSystem.Evolution.PokemonAggregate
{
    public class Pokemon : Entity, IAggregateRoot
    {
        public Pokemon(Species pokemonSpecies, Stats stats, int experience, int level)
        {
            PokemonSpecies = pokemonSpecies ?? throw new ArgumentNullException(nameof(pokemonSpecies));
            Stats = stats ?? throw new ArgumentNullException(nameof(stats));
            Experience = experience;
            Level = level;
        }

        public Species PokemonSpecies { get; private set; }
        public Stats Stats { get; private set; }
        public int Experience { get; private set; }
        public int Level { get; private set; }
    }
}
