using PokemonSystem.Common.ValueObjects;

namespace PokemonSystem.BillsPC.Application.ViewModel
{
    public class PokemonDetail
    {
        public Guid Id { get; protected set; }
        public string? Nickname { get; protected set; }
        public string Gender { get; protected set; }
        public string SpeciesName { get; protected set; }
        public uint Level { get; protected set; }
        public List<Move> LearntMoves { get; protected set; }
        public Stats Stats { get; protected set; }
    }
}
