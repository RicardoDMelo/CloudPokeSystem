
using PokemonSystem.Common.Enums;
using PokemonSystem.Common.SeedWork.Domain;
using PokemonSystem.Common.ValueObjects;
using System;

namespace PokemonSystem.Evolution.Domain.PokemonAggregate
{
    public class EvolutionCriteria : Entity
    {
        public EvolutionCriteria(EvolutionType evolutionType, Level? minimumLevel, Species evolutionSpecies)
        {
            EvolutionType = evolutionType;
            if (evolutionType == EvolutionType.Level && minimumLevel is null)
            {
                throw new ArgumentNullException(nameof(minimumLevel));
            }
            MinimumLevel = minimumLevel;
            EvolutionSpecies = evolutionSpecies ?? throw new ArgumentNullException(nameof(evolutionSpecies));
        }

        public EvolutionType EvolutionType { get; private set; }
        public Level? MinimumLevel { get; private set; }
        public Species EvolutionSpecies { get; private set; }

        public bool CanEvolveByLevel(Level pokemonLevel)
        {
            return EvolutionType.Level == EvolutionType && MinimumLevel! <= pokemonLevel;
        }
    }
}
