using PokemonSystem.Common.SeedWork.Domain;
using PokemonSystem.Common.ValueObjects;

namespace PokemonSystem.BillsPC.Domain.SpeciesAggregate
{
    public class Species : Entity<uint>
    {
        public Species(uint id, string name, Typing typing) : base(id)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Typing = typing ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; private set; }
        public Typing Typing { get; private set; }
    }
}
