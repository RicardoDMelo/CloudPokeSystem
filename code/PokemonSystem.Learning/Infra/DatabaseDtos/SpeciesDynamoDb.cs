using Amazon.DynamoDBv2.DataModel;

namespace PokemonSystem.Learning.Infra.DatabaseDtos
{
    [DynamoDBTable(DatabaseConsts.POKEMON_SPECIES_TABLE)]
    public class SpeciesDynamoDb
    {
        [DynamoDBHashKey]
        public uint Id { get; set; }

        [DynamoDBLocalSecondaryIndexRangeKey]
        public string Name { get; set; } = string.Empty;
        public List<MoveByLevelDynamoDb> MoveSet { get; set; } = new List<MoveByLevelDynamoDb>();
    }
}
