using MediatR;

namespace PokemonSystem.Learning.Domain.PokemonAggregate
{
    public class PokemonLearnedMovesDomainEvent : INotification
    {
        public PokemonLearnedMovesDomainEvent(Pokemon pokemon)
        {
            Pokemon = pokemon;
        }

        public Pokemon Pokemon { get; }
    }
}
