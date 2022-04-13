namespace PokemonSystem.Incubator.Infra.DatabaseDtos
{
    public class StatsDynamoDb
    {
        public uint HP { get; set; }
        public uint Attack { get; set; }
        public uint Defense { get; set; }
        public uint SpecialAttack { get; set; }
        public uint SpecialDefense { get; set; }
        public uint Speed { get; set; }
    }
}
