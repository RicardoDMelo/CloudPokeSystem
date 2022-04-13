using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Evolution.Domain.PokemonAggregate;
using PokemonSystem.Tests.ValueObjects;

namespace PokemonSystem.Tests.Evolution.Builders
{
    public class EvolutionCriteriaBuilder
    {
        private EvolutionType _evolutionType = EvolutionType.Level;
        private Level? _minimumLevel = Levels.Ten;
        private string? _item = null;
        private Species _evolutionSpecies;

        public EvolutionCriteriaBuilder()
        {
            _evolutionSpecies = new SpeciesBuilder().Build();
        }

        public void Reset()
        {
            _evolutionType = EvolutionType.Level;
            _minimumLevel = Levels.Ten;
            _evolutionSpecies = new SpeciesBuilder().Build();
        }

        public EvolutionCriteriaBuilder WithMinimumLevel(Level level)
        {
            _evolutionType = EvolutionType.Level;
            _minimumLevel = level;
            _item = null;
            return this;
        }

        public EvolutionCriteriaBuilder WithItem(string item)
        {
            _evolutionType = EvolutionType.Item;
            _minimumLevel = null;
            _item = item;
            return this;
        }

        public EvolutionCriteriaBuilder WithTrading()
        {
            _evolutionType = EvolutionType.Trading;
            _item = null;
            _minimumLevel = null;
            return this;
        }

        public EvolutionCriteriaBuilder WithSpecies(Species species)
        {
            _evolutionSpecies = species;
            return this;
        }

        public EvolutionCriteria Build()
        {
            EvolutionCriteria evolutionCriteria;
            switch (_evolutionType)
            {
                case EvolutionType.Level:
                    evolutionCriteria = EvolutionCriteria.CreateLevelEvolution(_minimumLevel!, _evolutionSpecies);
                    break;
                case EvolutionType.Item:
                    evolutionCriteria = EvolutionCriteria.CreateItemEvolution(_item!, _evolutionSpecies);
                    break;
                case EvolutionType.Trading:
                    evolutionCriteria = EvolutionCriteria.CreateTradingEvolution(_evolutionSpecies);
                    break;
                default:
                    evolutionCriteria = EvolutionCriteria.CreateLevelEvolution(_minimumLevel!, _evolutionSpecies);
                    break;
            }

            Reset();
            return evolutionCriteria;
        }
    }
}