using Amazon.DynamoDBv2.DataModel;

namespace PokemonSystem.Evolution.Infra.DatabaseDtos
{
    [DynamoDBTable(DatabaseConsts.POKEMON_SPECIES_TABLE)]
    public class SpeciesDynamoDb
    {
        [DynamoDBHashKey]
        public uint Id { get; set; }

        [DynamoDBLocalSecondaryIndexRangeKey]
        public string Name { get; set; } = string.Empty;

        public StatsDynamoDb? BaseStats { get; set; }
        public List<EvolutionCriteriaDynamoDb> EvolutionCriterias { get; set; } = new List<EvolutionCriteriaDynamoDb>();
    }
}
