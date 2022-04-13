using MediatR;
using PokemonSystem.Incubator.Domain.PokemonAggregate;

namespace PokemonSystem.Incubator.Application.Commands
{
    public class CreateRandomPokemon : IRequest<Pokemon>
    {
        public string Nickname { get; set; } = string.Empty;
        public uint LevelToGrow { get; set; } = 1;
    }
}
