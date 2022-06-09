namespace PokemonSystem.BillsPC.Application.ViewModel
{
    public class PokemonLookup
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public int SpeciesId { get; protected set; }
        public uint? Level { get; protected set; }
    }
}
