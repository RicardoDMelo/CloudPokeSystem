using PokemonSystem.PokedexInjector;


Console.WriteLine("Getting data from CSV...");
const string csvPath = @".\DataFiles";
var importedData = CsvImporter.GetData(csvPath);
Console.WriteLine("Data retrieved from CSV.");
Console.WriteLine($"{importedData.Species.Count()} species retrieved.");
Console.WriteLine("-----------------------------------------------");

Console.WriteLine("Converting data to domain...");
var species = SpeciesAdapter.ConvertToDomain(importedData);
Console.WriteLine($"Data converted.");
Console.WriteLine($"{species.Count()} species converted.");
Console.WriteLine("-----------------------------------------------");

Console.WriteLine("Connecting to DynamoDB...");
var client = DynamoConnector.CreateClient(ConnectionType.Local);
Console.WriteLine("Connected to DynamoDB.");
Console.WriteLine("-----------------------------------------------");

Console.WriteLine("Creating Tables...");
await DatabaseStructure.CreateTablesAsync(client);
Console.WriteLine("Tables up and running.");
Console.WriteLine("-----------------------------------------------");

