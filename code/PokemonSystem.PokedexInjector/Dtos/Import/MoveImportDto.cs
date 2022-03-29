
using CsvHelper.Configuration.Attributes;
using PokemonSystem.Common.Enums;

namespace PokemonSystem.PokedexInjector
{
    public class MoveImportDto
    {
        public MoveImportDto()
        {
            Name = String.Empty;
            Type = PokemonType.Normal;
            CategoryText = String.Empty;
            PowerText = String.Empty;
            AccuracyText = String.Empty;
            PP = 0;
        }

        [Name("move")]
        public string Name { get; set; }

        [Name("type")]
        public PokemonType Type { get; set; }

        [Name("category")]
        public string CategoryText { get; set; }

        [Name("power")]
        public string PowerText { get; set; }

        [Name("accuracy")]
        public string AccuracyText { get; set; }

        [Name("pp")]
        public uint PP { get; set; }

        [Ignore]
        public uint? Power { get => PowerText == "—" ? 0 : Convert.ToUInt32(PowerText); }

        [Ignore]
        public double Accuracy { get => AccuracyText == "—" ? 0 : Convert.ToDouble(AccuracyText.Replace("%", string.Empty)) / 100; }

        [Ignore]
        public MoveCategory Category
        {
            get
            {
                if (Enum.TryParse<MoveCategory>(CategoryText, out var value))
                {
                    return value;
                }
                else
                {
                    return MoveCategory.Physical;
                }
            }
        }
    }
}
