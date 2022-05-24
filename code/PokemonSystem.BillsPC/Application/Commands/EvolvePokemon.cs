using MediatR;
using PokemonSystem.Common.ValueObjects;

namespace PokemonSystem.BillsPC.Application.Commands
{
    public class EvolvePokemon : INotification
    {
        public Guid Id { get; set; }
        public uint SpeciesId { get; set; }
        public Stats Stats { get; set; }
    }
}
