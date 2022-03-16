using CsvHelper;
using CsvHelper.Configuration;
using PokemonSystem.PokedexInjector;
using System.Globalization;


Console.WriteLine("Starting import to DynamoDB...");


var csvPath = @".\DataFiles";
var pokemonCsv = Path.Combine(csvPath, "pokemon.csv");
var movesetCsv = Path.Combine(csvPath, "moveset.csv");

var config = new CsvConfiguration(CultureInfo.InvariantCulture);

ICollection<SpeciesImportDto> importedSpecies;
ICollection<MoveSetImportDto> importedMovesets = new List<MoveSetImportDto>();

using (var reader = new StreamReader(pokemonCsv))
using (var csv = new CsvReader(reader, config))
{
    importedSpecies = csv
        .GetRecords<SpeciesImportDto>()
        .OrderBy(x => x.Line)
        .DistinctBy(x => x.Id)
        .ToList();

}

const string moveHeaderPrefix = "move";

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
                record.Moves.Add(move);
            }
        }

        importedMovesets.Add(record);
    }
}

foreach (var species in importedSpecies)
{
    var moveset = importedMovesets.First(x=>x.Id == species.Id);
    Console.WriteLine($"{species.Id} - {species.Name} | Moves: {moveset.Moves.Count}");
}


//var client = DynamoConnector.CreateClient(true);
