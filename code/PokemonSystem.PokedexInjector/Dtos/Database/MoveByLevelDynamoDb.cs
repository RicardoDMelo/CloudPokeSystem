namespace PokemonSystem.PokedexInjector.Dtos.Database
{
    internal class MoveByLevelDynamoDb
    {
        public uint Level { get; set; }
        public MoveDynamoDb? Move { get; set; }
    }
}
