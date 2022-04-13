using MediatR;

namespace PokemonSystem.Evolution.Domain.PokemonAggregate
{
    public class PokemonEvolvedDomainEvent : INotification
    {
        public PokemonEvolvedDomainEvent(Pokemon pokemon)
        {
            Pokemon = pokemon ?? throw new ArgumentNullException(nameof(pokemon));
        }

        public Pokemon Pokemon { get; private set; }
    }
}
