
using PokemonSystem.Common.Enums;
using PokemonSystem.Common.SeedWork;
using PokemonSystem.Common.ValueObjects;
using System;

namespace PokemonSystem.Incubator.Domain.SpeciesAggregate
{
    public class EvolutionCriteria : Entity
    {
        public EvolutionCriteria(EvolutionType evolutionType, Level minimumLevel, Species evolutionSpecies) : base()
        {
            EvolutionType = evolutionType;
            if (evolutionType == EvolutionType.Level && minimumLevel == null)
            {
                throw new ArgumentNullException(nameof(minimumLevel));
            }
            MinimumLevel = minimumLevel;
            EvolutionSpecies = evolutionSpecies ?? throw new ArgumentNullException(nameof(evolutionSpecies));
        }

        public EvolutionType EvolutionType { get; private set; }
        public Level MinimumLevel { get; private set; }
        public Species EvolutionSpecies { get; private set; }
    }
}
