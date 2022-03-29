namespace PokemonSystem.PokedexInjector.Dtos.Database
{
    internal class EvolutionCriteriaDynamoDb
    {
        public short EvolutionType { get; set; }
        public uint? MinimumLevel { get; set; }
        public string Item { get; set; } = string.Empty;
        public SpeciesDynamoDb? Species { get; set; }
    }
}
