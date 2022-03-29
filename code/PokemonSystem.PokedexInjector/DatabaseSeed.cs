using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using PokemonSystem.PokedexInjector.Dtos.Database;

namespace PokemonSystem.PokedexInjector
{
    internal class DatabaseSeed
    {
        public const string POKEMON_SPECIES_TABLE = "PokemonSpecies";
        public const string POKEMON_SPECIES_PRIMARY_KEY_NAME = "Id";
        public const string POKEMON_SPECIES_SORT_KEY_NAME = "Name";
        private const int WAIT_TIMEOUT = 1000;

        public static async Task CreateTablesAsync(IAmazonDynamoDB client)
        {
            var speciesRequest = PrepareTableCreation(POKEMON_SPECIES_TABLE, POKEMON_SPECIES_PRIMARY_KEY_NAME, POKEMON_SPECIES_SORT_KEY_NAME);
            try
            {
                await client.DeleteTableAsync(speciesRequest.TableName);
                await WaitUntilTableDeletedAsync(speciesRequest.TableName, client);
            }
            catch (ResourceNotFoundException) { }

            try
            {
                await client.CreateTableAsync(speciesRequest);
                await WaitUntilTableReadyAsync(speciesRequest.TableName, client); ;
                Console.WriteLine($"{speciesRequest.TableName} table created.");
            }
            catch (ResourceInUseException ex)
            {
                Console.WriteLine("Table already created.", ex.Message);
            }
        }

        public static async Task LoadDataAsync(IEnumerable<SpeciesDynamoDb> speciesList, IAmazonDynamoDB dbClient)
        {
            var context = new DynamoDBContext(dbClient);
            var batchWrite = context.CreateBatchWrite<SpeciesDynamoDb>();
            batchWrite.AddPutItems(speciesList);
            await batchWrite.ExecuteAsync();
        }

        public static async Task<int> GetDatabaseCount(IAmazonDynamoDB dbClient)
        {
            var context = new DynamoDBContext(dbClient);
            string? paginationToken = null;
            Search response;
            int count = 0;
            do
            {
                var queryOperation = new ScanOperationConfig()
                {
                    Select = SelectValues.Count,
                    PaginationToken = paginationToken
                };

                response = context.GetTargetTable<SpeciesDynamoDb>().Scan(queryOperation);

                await response.GetNextSetAsync();
                count += response.Count;
                paginationToken = response.PaginationToken;
            } while (!response.IsDone);
            return count;
        }

        private static CreateTableRequest PrepareTableCreation(string tableName, string keyName, string sortKeyName)
        {
            AttributeDefinition primaryKeyAttribute = new(keyName, ScalarAttributeType.N);

            KeySchemaElement primaryKey = new(keyName, KeyType.HASH);

            return new CreateTableRequest(tableName, new List<KeySchemaElement>() { primaryKey })
            {
                AttributeDefinitions = new List<AttributeDefinition>() { primaryKeyAttribute },
                BillingMode = BillingMode.PROVISIONED,
                ProvisionedThroughput = new ProvisionedThroughput(5, 1)
            };
        }

        private static async Task WaitUntilTableDeletedAsync(string tableName, IAmazonDynamoDB client)
        {
            string status = string.Empty;
            bool firstTry = true;
            do
            {
                if (!firstTry)
                {
                    Thread.Sleep(WAIT_TIMEOUT);
                }
                firstTry = false;
                var res = await client.DescribeTableAsync(new DescribeTableRequest
                {
                    TableName = tableName
                });
                status = res.Table.TableStatus;
            } while (status != String.Empty);
        }

        private static async Task WaitUntilTableReadyAsync(string tableName, IAmazonDynamoDB client)
        {
            string status = string.Empty;
            bool firstTry = true;
            do
            {
                if (!firstTry)
                {
                    Thread.Sleep(WAIT_TIMEOUT);
                }
                firstTry = false;
                try
                {
                    var res = await client.DescribeTableAsync(new DescribeTableRequest
                    {
                        TableName = tableName
                    });
                    status = res.Table.TableStatus;
                }
                catch (ResourceNotFoundException) { }
            } while (status != "ACTIVE");
        }
    }
}
