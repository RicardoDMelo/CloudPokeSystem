using Amazon.DynamoDBv2.DataModel;

namespace PokemonSystem.Learning.Infra.DatabaseDtos
{
    [DynamoDBTable(DatabaseConsts.POKEMON_TABLE)]
    public class PokemonDynamoDb
    {
        [DynamoDBHashKey]
        public string Id { get; set; } = string.Empty;
        public SpeciesDynamoDb? PokemonSpecies { get;  set; }
        public uint Level { get;  set; }
        public List<MoveDynamoDb> LearntMoves { get; set; } = new List<MoveDynamoDb>();
    }
}
