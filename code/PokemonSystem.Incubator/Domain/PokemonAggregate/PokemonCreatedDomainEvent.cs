using MediatR;

namespace PokemonSystem.Incubator.Domain.PokemonAggregate
{
    public class PokemonCreatedDomainEvent : INotification
    {
        public PokemonCreatedDomainEvent(Pokemon pokemon)
        {
            Pokemon = pokemon;
        }

        public Pokemon Pokemon { get; }
    }
}
