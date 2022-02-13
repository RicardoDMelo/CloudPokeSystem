using PokemonSystem.Common.SeedWork;
using PokemonSystem.Common.ValueObjects;
using System.Collections.Generic;

namespace PokemonSystem.Incubator.SpeciesAggregate
{
    public class Species : Entity, IAggregateRoot
    {
        public Species(int number, string name, Typing typing, Stats baseStats, double maleFactor, Species evolutionSpecies, List<MoveByLevel> moveSet)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new System.ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            }

            Number = number;
            Name = name;
            Typing = typing;
            BaseStats = baseStats ?? throw new System.ArgumentNullException(nameof(baseStats));
            MaleFactor = maleFactor;
            EvolutionSpecies = evolutionSpecies;
            _moveSet = moveSet ?? throw new System.ArgumentNullException(nameof(moveSet));
        }

        public int Number { get; private set; }
        public string Name { get; private set; }
        public Typing Typing { get; private set; }
        public Stats BaseStats { get; private set; }
        public double MaleFactor { get; private set; }
        public Species EvolutionSpecies { get; private set; }
        protected List<MoveByLevel> _moveSet { get; set; }
        public IReadOnlyCollection<MoveByLevel> MoveSet { get => _moveSet.AsReadOnly(); }
    }
}
