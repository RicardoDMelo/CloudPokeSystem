namespace PokemonSystem.Incubator.Infra.Database
{
    public class EvolutionCriteriaDynamoDb
    {
        public string Id { get; set; } = string.Empty;
        public short EvolutionType { get; set; }
        public uint? MinimumLevel { get; set; }
        public string? Item { get; set; } = null;
        public SpeciesDynamoDb? Species { get; set; }
    }
}
