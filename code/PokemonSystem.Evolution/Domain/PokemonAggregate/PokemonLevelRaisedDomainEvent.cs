using MediatR;

namespace PokemonSystem.Evolution.Domain.PokemonAggregate
{
    public class PokemonLevelRaisedDomainEvent : INotification
    {
        public PokemonLevelRaisedDomainEvent(Pokemon pokemon)
        {
            Pokemon = pokemon ?? throw new ArgumentNullException(nameof(pokemon));
        }

        public Pokemon Pokemon { get; private set; }
    }
}
