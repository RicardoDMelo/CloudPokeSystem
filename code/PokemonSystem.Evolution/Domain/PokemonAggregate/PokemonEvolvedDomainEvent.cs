using MediatR;

namespace PokemonSystem.Evolution.Domain.PokemonAggregate
{
    public class PokemonEvolvedDomainEvent : INotification
    {
        public PokemonEvolvedDomainEvent(Pokemon pokemon, Guid pokemonId, uint level, uint speciesId)
        {
            Pokemon = pokemon;
            PokemonId = pokemonId;
            Level = level;
            SpeciesId = speciesId;
        }

        public Pokemon Pokemon { get; private set; }
        public Guid PokemonId { get; private set; }
        public uint Level { get; private set; }
        public uint SpeciesId { get; private set; }
    }
}
