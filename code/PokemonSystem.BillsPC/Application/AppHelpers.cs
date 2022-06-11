using System.Text.Json;

namespace PokemonSystem.BillsPC.Application
{
    internal static class AppHelpers
    {
        internal static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        internal static readonly Dictionary<string, string> Headers = new Dictionary<string, string> {
                        {"Access-Control-Allow-Headers", "Content-Type" },
                        {"Access-Control-Allow-Origin", "*"},
                        {"Access-Control-Allow-Methods", "OPTIONS,POST,GET"} };
    }
}
