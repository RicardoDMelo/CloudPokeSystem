using PokemonSystem.Common.SeedWork;
using System.Collections.Generic;

namespace PokemonSystem.Incubator.SpeciesAggregate
{
    public class Species : Entity, IAggregateRoot
    {
        protected List<MoveByLevel> _moveSet { get; set; }
        public IReadOnlyCollection<MoveByLevel> MoveSet { get => _moveSet.AsReadOnly(); }
    }
}
