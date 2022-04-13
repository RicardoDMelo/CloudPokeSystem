using PokemonSystem.Common.SeedWork.Domain;
using PokemonSystem.Common.ValueObjects;

namespace PokemonSystem.Evolution.Domain.PokemonAggregate
{
    public class Species : Entity<uint>
    {
        public Species(uint id, string name, Stats baseStats, List<EvolutionCriteria> evolutionCriterias) : base(id)
        {
            Name = name;
            BaseStats = baseStats;
            _evolutionCriterias = evolutionCriterias ?? throw new ArgumentNullException(nameof(evolutionCriterias));
        }

        public string Name { get; private set; }
        public Stats BaseStats { get; private set; }
        protected List<EvolutionCriteria> _evolutionCriterias { get; set; }
        public IReadOnlyCollection<EvolutionCriteria> EvolutionCriterias { get => _evolutionCriterias.AsReadOnly(); }
    }
}
