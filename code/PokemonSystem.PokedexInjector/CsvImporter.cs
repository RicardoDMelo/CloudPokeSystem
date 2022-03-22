using CsvHelper;
using CsvHelper.Configuration;
using PokemonSystem.PokedexInjector.Dtos;
using System.Globalization;

namespace PokemonSystem.PokedexInjector
{
    internal static class CsvImporter
    {
        const string moveHeaderPrefix = "move";

        internal static ImportDto GetData(string csvPath)
        {
            var pokemonCsv = Path.Combine(csvPath, "pokemon.csv");
            var movesetCsv = Path.Combine(csvPath, "moveset.csv");
            var movesCsv = Path.Combine(csvPath, "moves.csv");
            var evolutionsCsv = Path.Combine(csvPath, "evolutions.csv");


            var importedSpecies = GetSpecies(pokemonCsv);
            var importedMovesets = GetMovesets(movesetCsv);
            var importedMoves = GetMoves(movesCsv);
            var importedEvolution = GetEvolutions(evolutionsCsv);

            return new(importedSpecies, importedMovesets, importedMoves, importedEvolution);
        }

        private static ICollection<SpeciesImportDto> GetSpecies(string pokemonCsv)
        {
            ICollection<SpeciesImportDto> importedSpecies;
            using (var reader = new StreamReader(pokemonCsv))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                importedSpecies = csv
                    .GetRecords<SpeciesImportDto>()
                    .OrderBy(x => x.Line)
                    .DistinctBy(x => x.Id)
                    .ToList();
            }

            return importedSpecies;
        }

        private static ICollection<MoveSetImportDto> GetMovesets(string movesetCsv)
        {
            ICollection<MoveSetImportDto> importedMovesets = new List<MoveSetImportDto>();
            using (var reader = new StreamReader(movesetCsv))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                var moveHeaders = csv.HeaderRecord.Where(x => x.StartsWith(moveHeaderPrefix));

                while (csv.Read())
                {
                    var record = new MoveSetImportDto
                    {
                        Id = csv.GetField<int>("ndex"),
                        SpeciesName = csv.GetField("species")
                    };

                    foreach (var moveHeader in moveHeaders)
                    {
                        string? move = csv.GetField(moveHeader);
                        if (!string.IsNullOrEmpty(move))
                        {
                            record.MovesWithLevel.Add(move);
                        }
                    }

                    importedMovesets.Add(record);
                }
            }
            return importedMovesets;
        }

        private static ICollection<MoveImportDto> GetMoves(string movesCsv)
        {
            ICollection<MoveImportDto> importedMoves;
            using (var reader = new StreamReader(movesCsv))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                importedMoves = csv
                    .GetRecords<MoveImportDto>()
                    .ToList();
            }

            return importedMoves;
        }

        private static ICollection<EvolutionImportDto> GetEvolutions(string evolutionsCsv)
        {
            ICollection<EvolutionImportDto> importedEvolutions;
            using (var reader = new StreamReader(evolutionsCsv))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                importedEvolutions = csv
                    .GetRecords<EvolutionImportDto>()
                    .ToList();
            }

            return importedEvolutions;
        }

    }
}
