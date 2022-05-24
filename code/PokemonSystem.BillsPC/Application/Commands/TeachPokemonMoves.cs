using MediatR;
using PokemonSystem.Common.Enums;

namespace PokemonSystem.BillsPC.Application.Commands
{
    public class TeachPokemonMoves : INotification
    {
        public Guid Id { get; set; }
        public List<MoveDto> LearntMoves { get; set; }
    }

    public class MoveDto
    {
        public string Name { get; set; }
        public PokemonType Type { get; set; }
        public MoveCategory Category { get; set; }
        public uint? Power { get; set; }
        public double Accuracy { get; set; }
        public uint PP { get; set; }
    }
}
