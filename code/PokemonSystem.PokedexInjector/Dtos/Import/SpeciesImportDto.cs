using CsvHelper.Configuration.Attributes;
using PokemonSystem.Common.Enums;

namespace PokemonSystem.PokedexInjector
{
    public class SpeciesImportDto
    {
        public SpeciesImportDto()
        {
            Id = 0;
            Name = String.Empty;
            Type1 = PokemonType.Normal;
            HP = 0;
            Attack = 0;
            Defense = 0;
            SpecialAttack = 0;
            SpecialDefense = 0;
            Speed = 0;
            PreEvolution = String.Empty;
        }

        [Name("id")]
        public uint Line { get; set; }

        [Name("ndex")]
        public uint Id { get; set; }

        [Name("species")]
        public string Name { get; set; }

        [Name("type1")]
        public PokemonType Type1 { get; set; }

        [Name("type2")]
        public PokemonType? Type2 { get; set; }

        [Name("hp")]
        public uint HP { get; set; }

        [Name("attack")]
        public uint Attack { get; set; }

        [Name("defense")]
        public uint Defense { get; set; }

        [Name("spattack")]
        public uint SpecialAttack { get; set; }

        [Name("spdefense")]
        public uint SpecialDefense { get; set; }

        [Name("speed")]
        public uint Speed { get; set; }

        [Name("percent-male")]
        public double? MaleFactor { get; set; }

        [Name("pre-evolution")]
        public string PreEvolution { get; set; }
    }
}
