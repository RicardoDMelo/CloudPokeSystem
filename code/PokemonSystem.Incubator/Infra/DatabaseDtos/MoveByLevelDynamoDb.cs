namespace PokemonSystem.Incubator.Infra.DatabaseDtos
{
    public class MoveByLevelDynamoDb
    {
        public string Id { get; set; } = string.Empty;
        public uint? Level { get; set; }
        public MoveDynamoDb? Move { get; set; }
    }
}
