using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Evolution.Domain.PokemonAggregate;

namespace PokemonSystem.Tests.Evolution.Builders
{
    public class SpeciesBuilder
    {
        private int _number;
        private string _name;
        private Stats _baseStats;
        private EvolutionCriteria _evolutionCriteria;

        public SpeciesBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            _number = 128;
            _name = "Tauros";
            _baseStats = new Stats(1, 2, 3, 4, 5, 6);
            _evolutionCriteria = null;
        }

        public SpeciesBuilder WithNumber(int number)
        {
            _number = number;
            return this;
        }

        public SpeciesBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public SpeciesBuilder WithBaseStats(Stats baseStats)
        {
            _baseStats = baseStats;
            return this;
        }

        public SpeciesBuilder WithEvolutionCriteria(EvolutionType evolutionType, Level minimumLevel, Species evolutionSpecies)
        {
            _evolutionCriteria = new EvolutionCriteria(evolutionType, minimumLevel, evolutionSpecies);
            return this;
        }

        public Species Build()
        {
            var species = new Species(
                _number,
                _name,
                _baseStats,
                _evolutionCriteria
            );

            Reset();

            return species;
        }
    }
}