using PokemonSystem.Common.Properties;
using PokemonSystem.Common.SeedWork.Domain;
using PokemonSystem.Common.ValueObjects;

namespace PokemonSystem.Incubator.Domain.SpeciesAggregate
{
    public class Species : Entity<int>, IAggregateRoot
    {
        private const double MIN_MALE_FACTOR = 0;
        private const double MAX_MALE_FACTOR = 1;

        public Species(int id, string name, Typing typing, Stats baseStats, double? maleFactor, List<EvolutionCriteria> evolutionCriterias, List<MoveByLevel> moveSet) : base()
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            }

            if (maleFactor != null && (maleFactor < MIN_MALE_FACTOR || maleFactor > MAX_MALE_FACTOR))
            {
                throw new ArgumentException(string.Format(Errors.Between, nameof(maleFactor), MIN_MALE_FACTOR, MAX_MALE_FACTOR));
            }

            Id = id;
            Name = name;
            Typing = typing ?? throw new ArgumentNullException(nameof(typing));
            BaseStats = baseStats ?? throw new ArgumentNullException(nameof(baseStats));
            MaleFactor = maleFactor;
            _evolutionCriterias = evolutionCriterias ?? throw new ArgumentNullException(nameof(evolutionCriterias));
            _moveSet = moveSet ?? throw new ArgumentNullException(nameof(moveSet));
        }

        public string Name { get; private set; }
        public Typing Typing { get; private set; }
        public Stats BaseStats { get; private set; }
        public double? MaleFactor { get; private set; }
        protected List<EvolutionCriteria> _evolutionCriterias { get; set; }
        public IReadOnlyCollection<EvolutionCriteria> EvolutionCriterias { get => _evolutionCriterias.AsReadOnly(); }
        protected List<MoveByLevel> _moveSet { get; set; }
        public IReadOnlyCollection<MoveByLevel> MoveSet { get => _moveSet.AsReadOnly(); }
    }
}
