namespace PokemonSystem.PokedexInjector.Dtos
{
    internal class ImportDto
    {
        public ImportDto(IEnumerable<SpeciesImportDto> species, IEnumerable<MoveSetImportDto> moveSets, IEnumerable<MoveImportDto> moves, IEnumerable<EvolutionImportDto> evolutins)
        {
            Species = species;
            MoveSets = moveSets;
            Moves = moves;
            Evolutions = evolutins;
        }

        public IEnumerable<SpeciesImportDto> Species { get; private set; }
        public IEnumerable<MoveSetImportDto> MoveSets { get; private set; }
        public IEnumerable<MoveImportDto> Moves { get; private set; }
        public IEnumerable<EvolutionImportDto> Evolutions { get; private set; }
    }
}
