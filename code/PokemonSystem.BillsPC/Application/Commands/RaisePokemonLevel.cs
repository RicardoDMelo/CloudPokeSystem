using MediatR;
using PokemonSystem.Common.ValueObjects;

namespace PokemonSystem.BillsPC.Application.Commands
{
    public class RaisePokemonLevel : INotification
    {
        public Guid Id { get; set; }
        public uint Level { get; set; }
        public Stats Stats { get; set; }
    }
}
