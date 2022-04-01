using MediatR;
using PokemonSystem.Evolution.Domain.PokemonAggregate;

namespace PokemonSystem.Evolution.Domain.Events
{
    internal class PokemonLevelRaisedDomainEvent : INotification
    {
        public PokemonLevelRaisedDomainEvent(Pokemon pokemon)
        {
            Pokemon = pokemon ?? throw new ArgumentNullException(nameof(pokemon));
        }

        public Pokemon Pokemon { get; private set; }
    }
}
