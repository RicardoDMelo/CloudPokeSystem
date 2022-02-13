using PokemonSystem.Common.SeedWork;
using PokemonSystem.Common.ValueObjects;
using System;

namespace PokemonSystem.Evolution.PokemonAggregate
{
    public class Species : Entity
    {
        public Species(int number, string name, Stats baseStats, Species evolutionSpecies)
        {
            Number = number;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            BaseStats = baseStats ?? throw new ArgumentNullException(nameof(baseStats));
            EvolutionSpecies = evolutionSpecies ?? throw new ArgumentNullException(nameof(evolutionSpecies));
        }

        public int Number { get; private set; }
        public string Name { get; private set; }
        public Stats BaseStats { get; private set; }
        public Species EvolutionSpecies { get; private set; }
    }
}
