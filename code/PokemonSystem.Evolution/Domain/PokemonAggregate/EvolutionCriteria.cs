
using PokemonSystem.Common.Enums;
using PokemonSystem.Common.SeedWork.Domain;
using PokemonSystem.Common.ValueObjects;
using System;

namespace PokemonSystem.Evolution.Domain.PokemonAggregate
{
    public class EvolutionCriteria : Entity
    {
        protected EvolutionCriteria(EvolutionType evolutionType, Level? minimumLevel, string? item, Species species)
        {
            EvolutionType = evolutionType;
            MinimumLevel = minimumLevel;
            Item = item;
            Species = species;
        }

        public static EvolutionCriteria CreateLevelEvolution(Level minimumLevel, Species species)
        {
            return new EvolutionCriteria(EvolutionType.Level, minimumLevel, null, species);
        }

        public static EvolutionCriteria CreateItemEvolution(string item, Species species)
        {
            return new EvolutionCriteria(EvolutionType.Item, null, item, species);
        }

        public static EvolutionCriteria CreateTradingEvolution(Species species)
        {
            return new EvolutionCriteria(EvolutionType.Trading, null, null, species);
        }

        public EvolutionType EvolutionType { get; private set; }
        public Level? MinimumLevel { get; private set; }
        public string? Item { get; private set; }
        public Species Species { get; private set; }
    }
}
