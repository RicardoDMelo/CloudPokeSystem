namespace PokemonSystem.PokedexInjector
{
    internal class MoveSetImportDto
    {
        public MoveSetImportDto()
        {
            Id = 0;
            SpeciesName = String.Empty;
            MovesWithLevel = new List<string>();
        }

        public int Id { get; set; }
        public string SpeciesName { get; set; }
        public ICollection<string> MovesWithLevel { get; set; }
    }
}
