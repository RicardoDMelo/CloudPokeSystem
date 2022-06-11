using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Evolution.Domain.PokemonAggregate;

namespace PokemonSystem.Tests.Evolution.Builders
{
    public class SpeciesBuilder
    {
        private uint _number = 128;
        private string _name = "Tauros";
        private Stats _baseStats = new Stats(1, 2, 3, 4, 5, 6);
        private List<EvolutionCriteria> _evolutionCriterias = new List<EvolutionCriteria>();

        public SpeciesBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            _number = 128;
            _name = "Tauros";
            _baseStats = new Stats(1, 2, 3, 4, 5, 6);
            _evolutionCriterias = new List<EvolutionCriteria>();
        }

        public SpeciesBuilder WithNumber(uint number)
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

        public SpeciesBuilder WithEvolutionCriterias(List<EvolutionCriteria> evolutionCriterias)
        {
            _evolutionCriterias = evolutionCriterias;
            return this;
        }

        public Species Build()
        {
            var species = new Species(
                _number,
                _name,
                _baseStats,
                _evolutionCriterias
            );
            Reset();
            return species;
        }
    }
}