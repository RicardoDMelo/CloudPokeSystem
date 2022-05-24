using Amazon.DynamoDBv2.DataModel;
using PokemonSystem.BillsPC.Infra;

namespace PokemonSystem.Common.SeedWork.Domain
{
    [DynamoDBTable(DatabaseConsts.POKEMON_EVENTS_TABLE)]
    public  class EventRecordDb
    {
        public string Id { get;  set; } = string.Empty;
        public string Type { get;  set; } = string.Empty;
        [DynamoDBHashKey]
        public string StreamId { get;  set; } = string.Empty;
        [DynamoDBRangeKey]
        public int StreamPosition { get;  set; }
        public DateTime Timestamp { get;  set; }
        public string Data { get;  set; } = string.Empty;
    }
}
