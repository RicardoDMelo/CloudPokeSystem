using MediatR;
using PokemonSystem.Common.Enums;

namespace PokemonSystem.BillsPC.Application.Commands
{
    public class CreatePokemon : INotification
    {
        public Guid Id { get; set; }
        public string? Nickname { get; set; }
        public uint LevelToGrow { get; set; }
        public uint SpeciesId { get; set; }
        public Gender Gender { get; set; }
    }
}
