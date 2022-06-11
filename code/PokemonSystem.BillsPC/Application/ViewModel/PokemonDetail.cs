using PokemonSystem.Common.ValueObjects;

namespace PokemonSystem.BillsPC.Application.ViewModel
{
    public class PokemonDetail
    {
        public Guid Id { get; protected set; }
        public string? Nickname { get; protected set; }
        public string Gender { get; protected set; } = string.Empty;
        public int SpeciesId { get; protected set; }
        public string SpeciesName { get; protected set; } = string.Empty;
        public short Type1 { get; protected set; }
        public short? Type2 { get; protected set; }
        public uint Level { get; protected set; }
        public List<Move> LearntMoves { get; protected set; } = new List<Move>();
        public Stats Stats { get; protected set; } = new Stats(0, 0, 0, 0, 0, 0);
    }
}
