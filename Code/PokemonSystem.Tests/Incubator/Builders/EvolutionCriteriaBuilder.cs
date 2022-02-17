using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Incubator.Domain.SpeciesAggregate;
using PokemonSystem.Tests.ValueObjects;

namespace PokemonSystem.Tests.Incubator.Builders
{
    public class EvolutionCriteriaBuilder
    {
        private EvolutionType _evolutionType;
        private Level _minimumLevel;
        private Species _evolutionSpecies;

        public EvolutionCriteriaBuilder()
        {            
            _evolutionType = EvolutionType.Level;
            _minimumLevel = Levels.Ten;
            _evolutionSpecies = new SpeciesBuilder().Build();
        }

        public EvolutionCriteriaBuilder WithEvolutionType(EvolutionType evolutionType)
        {
            _evolutionType = evolutionType;
            return this;
        }

        public EvolutionCriteriaBuilder WithMinimumLevel(Level level)
        {
            _minimumLevel = level;
            return this;
        }

        public EvolutionCriteriaBuilder WithSpecies(Species species)
        {
            _evolutionSpecies = species;
            return this;
        }

        public EvolutionCriteria Build()
        {
            return new EvolutionCriteria(_evolutionType, _minimumLevel, _evolutionSpecies);
        }
    }
}