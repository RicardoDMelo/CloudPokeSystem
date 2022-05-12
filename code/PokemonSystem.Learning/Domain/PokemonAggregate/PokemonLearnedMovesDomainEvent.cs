using MediatR;
using PokemonSystem.Learning.Domain.PokemonAggregate;

namespace PokemonSystem.Incubator.Domain.PokemonAggregate
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
