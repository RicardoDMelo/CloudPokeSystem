using MediatR;
using PokemonSystem.Incubator.Application.ViewModel;

namespace PokemonSystem.Incubator.Application.Commands
{
    public class CreateRandomPokemon : IRequest<PokemonLookup>
    {
        public string Nickname { get; set; } = string.Empty;
        public uint LevelToGrow { get; set; } = 1;
    }
}
