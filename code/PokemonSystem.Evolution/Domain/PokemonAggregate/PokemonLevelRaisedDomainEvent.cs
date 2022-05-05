using MediatR;

namespace PokemonSystem.Evolution.Domain.PokemonAggregate
{
    public class PokemonLevelRaisedDomainEvent : INotification
    {
        public PokemonLevelRaisedDomainEvent(Pokemon pokemon, Guid id, uint level, uint speciesId)
        {
            Pokemon = pokemon;
            Id = id;
            Level = level;
            SpeciesId = speciesId;
        }

        public Pokemon Pokemon { get; private set; }
        public Guid Id { get; private set; }
        public uint Level { get; private set; }
        public uint SpeciesId { get; private set; }
    }
}
