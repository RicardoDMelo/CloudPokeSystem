namespace PokemonSystem.Incubator.Infra.DatabaseDtos
{
    public class MoveDynamoDb
    {
        public string Name { get; set; } = string.Empty;
        public short Type { get; set; }
        public short Category { get; set; }
        public uint? Power { get; set; }
        public double Accuracy { get; set; }
        public uint PP { get; set; }
    }
}
