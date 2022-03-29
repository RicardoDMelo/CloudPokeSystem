
using CsvHelper.Configuration.Attributes;
using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;

namespace PokemonSystem.PokedexInjector
{
    public class EvolutionImportDto
    {
        public EvolutionImportDto()
        {
            From = String.Empty;
            To = String.Empty;
            ConditionText = String.Empty;
            Type = EvolutionType.Level;
        }

        [Name("from")]
        public string From { get; set; }

        [Name("to")]
        public string To { get; set; }

        [Name("condition")]
        public string ConditionText { get; set; }

        [Name("type")]
        public EvolutionType Type { get; set; }

        [Ignore]
        public Level? Level { get => Type == EvolutionType.Level ? new Level(Convert.ToUInt32(ConditionText)) : null; }

        [Ignore]
        public string? Item { get => Type == EvolutionType.Item ? ConditionText : null; }
    }
}
