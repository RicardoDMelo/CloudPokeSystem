using Amazon.DynamoDBv2.DataModel;
using PokemonSystem.BillsPC.Infra;

namespace PokemonSystem.BillsPC.Infra.DatabaseDtos
{
    [DynamoDBTable(DatabaseConsts.POKEMON_HISTORY_TABLE)]
    public class PokemonHistoryDb
    {
        [DynamoDBHashKey]
        public string Id { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}
