using PokemonSystem.Common.SeedWork;
using PokemonSystem.Common.ValueObjects;
using System;

namespace PokemonSystem.Evolution.Domain.PokemonAggregate
{
    public class Species : Entity
    {
        public Species(int number, string name, Stats baseStats, EvolutionCriteria evolutionCriteria)
        {
            Number = number;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            BaseStats = baseStats ?? throw new ArgumentNullException(nameof(baseStats));
            EvolutionCriteria = evolutionCriteria;
        }

        public int Number { get; private set; }
        public string Name { get; private set; }
        public Stats BaseStats { get; private set; }
        public EvolutionCriteria EvolutionCriteria { get; private set; }
    }
}
