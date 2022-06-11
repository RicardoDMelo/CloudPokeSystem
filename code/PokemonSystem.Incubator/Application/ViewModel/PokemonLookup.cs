namespace PokemonSystem.Incubator.Application.ViewModel
{
    public class PokemonLookup
    {
        public Guid Id { get; protected set; }
        public uint SpeciesId { get; protected set; }
        public string Name { get; protected set; } = string.Empty;
        public uint? Level { get; protected set; }
    }
}
