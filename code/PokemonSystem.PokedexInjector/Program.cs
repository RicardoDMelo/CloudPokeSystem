using PokemonSystem.PokedexInjector;

var connectionType = ConnectionType.Local;
if (args.Length > 0)
{
    Console.WriteLine($"Argument: {args[0]}");
    if (args[0].Equals("remote"))
    {
        connectionType = ConnectionType.Remote;
    }
}
else
{
    Console.WriteLine("No argument.");
}

Console.WriteLine("Connecting to DynamoDB...");
var client = DynamoConnector.CreateClient(connectionType);
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
Console.WriteLine($"Data converted.");
Console.WriteLine($"{species.Count()} species converted.");
Console.WriteLine("-----------------------------------------------");

if (connectionType == ConnectionType.Local)
{
    Console.WriteLine("Creating Tables...");
    await DatabaseSeed.CreateTablesAsync(client);
    Console.WriteLine("Tables up and running.");
    Console.WriteLine("-----------------------------------------------");
}

Console.WriteLine("Adding data to database...");
var dynamoDbData = speciesAdapter.ConvertToDto(species);
await DatabaseSeed.LoadDataAsync(dynamoDbData, client);
Console.WriteLine("DATABASE COMPLETE!");
Console.WriteLine("-----------------------------------------------");

var databaseCount = await DatabaseSeed.GetDatabaseCount(client);
Console.WriteLine($"{databaseCount} itens added.");
