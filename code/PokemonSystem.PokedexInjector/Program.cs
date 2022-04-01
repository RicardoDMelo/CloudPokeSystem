using PokemonSystem.PokedexInjector;

Console.WriteLine("Connecting to DynamoDB...");
var databaseSeed = new DatabaseSeed();
Console.WriteLine("Connected to DynamoDB.");
Console.WriteLine("-----------------------------------------------");


Console.WriteLine("Getting data from CSV...");
string csvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @".\DataFiles");
var importedData = CsvImporter.GetData(csvPath);

Console.WriteLine("Data retrieved from CSV.");
Console.WriteLine($"{importedData.Species.Count()} species retrieved.");
Console.WriteLine("-----------------------------------------------");

Console.WriteLine("Converting data to domain...");
var speciesAdapter = new SpeciesAdapter();
var species = speciesAdapter.ConvertToDomain(importedData);
var dynamoDbData = speciesAdapter.ConvertToDto(species);
Console.WriteLine($"Data converted.");
Console.WriteLine($"{dynamoDbData.Count()} species converted.");
Console.WriteLine("-----------------------------------------------");

Console.WriteLine("Creating Tables...");
await databaseSeed.CreateTablesAsync();
Console.WriteLine("Tables up and running.");
Console.WriteLine("-----------------------------------------------");

Console.WriteLine("Adding data to database...");
await databaseSeed.LoadDataAsync(dynamoDbData);
Console.WriteLine("DATABASE COMPLETE!");
Console.WriteLine("-----------------------------------------------");

var databaseCount = await databaseSeed.GetDatabaseCountAsync();
Console.WriteLine($"{databaseCount} itens added.");
