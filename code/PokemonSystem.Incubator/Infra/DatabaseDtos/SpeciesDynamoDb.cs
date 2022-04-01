using Amazon.DynamoDBv2.DataModel;

namespace PokemonSystem.Incubator.Infra.Database
{
    [DynamoDBTable(DatabaseConsts.POKEMON_SPECIES_TABLE)]
    public class SpeciesDynamoDb
    {
        [DynamoDBHashKey]
        public int Id { get; set; }

        [DynamoDBLocalSecondaryIndexRangeKey]
        public string Name { get; set; } = string.Empty;

        public TypingDynamoDb? Typing { get; set; }
        public StatsDynamoDb? BaseStats { get; set; }
        public double? MaleFactor { get; set; }
        public List<EvolutionCriteriaDynamoDb> EvolutionCriterias { get; set; } = new List<EvolutionCriteriaDynamoDb>();
        public List<MoveByLevelDynamoDb> MoveSet { get; set; } = new List<MoveByLevelDynamoDb>();
    }
}
