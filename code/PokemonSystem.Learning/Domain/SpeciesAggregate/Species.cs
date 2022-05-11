using PokemonSystem.Common.SeedWork.Domain;

namespace PokemonSystem.Learning.Domain.SpeciesAggregate
{
    public class Species : Entity<uint>
    {
        public Species(uint id, string name, IEnumerable<MoveByLevel> moveSet) : base(id)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            _moveSet = moveSet.ToList() ?? throw new ArgumentNullException(nameof(moveSet));
        }

        public string Name { get; private set; }
        protected List<MoveByLevel> _moveSet { get; set; }
        public IReadOnlyCollection<MoveByLevel> MoveSet { get => _moveSet.AsReadOnly(); }
    }
}
