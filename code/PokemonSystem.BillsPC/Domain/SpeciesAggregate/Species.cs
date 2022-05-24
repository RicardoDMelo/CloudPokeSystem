using PokemonSystem.Common.SeedWork.Domain;

namespace PokemonSystem.BillsPC.Domain.SpeciesAggregate
{
    public class Species : Entity<uint>
    {
        public Species(uint id, string name) : base(id)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; private set; }
    }
}
