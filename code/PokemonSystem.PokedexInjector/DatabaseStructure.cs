using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace PokemonSystem.PokedexInjector
{
    internal static class DatabaseStructure
    {
        public static async Task CreateTablesAsync(AmazonDynamoDBClient client)
        {
            var speciesRequest = CreatePokemonSpeciesTableRequest();
            try
            {
                await client.DeleteTableAsync(speciesRequest.TableName);
            }
            catch (ResourceNotFoundException) { }

            try
            {
                await client.CreateTableAsync(speciesRequest);
                Console.WriteLine($"{speciesRequest.TableName} created.");
            }
            catch (ResourceInUseException ex)
            {
                Console.WriteLine("Table already created.", ex.Message);
            }
        }


        const string POKEMON_SPECIES_TABLE = "PokemonSpecies";
        const string POKEMON_SPECIES_PRIMARY_KEY_NAME = "PokemonNumber";
        const string POKEMON_SPECIES_SORT_KEY_NAME = "PokemonType";


        private static CreateTableRequest CreatePokemonSpeciesTableRequest()
        {
            AttributeDefinition primaryKeyAttribute = new(POKEMON_SPECIES_PRIMARY_KEY_NAME, ScalarAttributeType.N);
            AttributeDefinition sortKeyAttribute = new(POKEMON_SPECIES_SORT_KEY_NAME, ScalarAttributeType.N);

            KeySchemaElement primaryKey = new(POKEMON_SPECIES_PRIMARY_KEY_NAME, KeyType.HASH);
            KeySchemaElement sortKey = new(POKEMON_SPECIES_SORT_KEY_NAME, KeyType.RANGE);

            return new CreateTableRequest(POKEMON_SPECIES_TABLE, new List<KeySchemaElement>() { primaryKey, sortKey })
            {
                AttributeDefinitions = new List<AttributeDefinition>() { primaryKeyAttribute, sortKeyAttribute },
                BillingMode = BillingMode.PAY_PER_REQUEST
            };
        }
    }
}
