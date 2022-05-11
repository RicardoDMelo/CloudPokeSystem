using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using PokemonSystem.Incubator.Infra;
using PokemonSystem.Incubator.Infra.DatabaseDtos;
using PokemonSystem.Incubator.Infra.DataContracts;

namespace PokemonSystem.PokedexInjector
{
    internal class DatabaseSeed
    {
        private const int WAIT_TIMEOUT = 1000;

        private readonly IAppSpeciesRepository _speciesRepository;
        private readonly IDynamoDBContext _dynamoDBContext;
        private readonly IAmazonDynamoDB _dbClient;

        public DatabaseSeed()
        {
            _dbClient = new AmazonDynamoDBClient();
            _dynamoDBContext = new DynamoDBContext(_dbClient);
            _speciesRepository = new SpeciesRepository(_dynamoDBContext, new PokemonSystem.Incubator.Infra.Adapters.SpeciesAdapter());
        }

        public async Task CreateTablesAsync()
        {
            var speciesRequest = PrepareTableCreation(DatabaseConsts.POKEMON_SPECIES_TABLE, DatabaseConsts.POKEMON_SPECIES_PRIMARY_KEY_NAME);
            try
            {
                await _dbClient.DeleteTableAsync(speciesRequest.TableName);
                await WaitUntilTableDeletedAsync(speciesRequest.TableName);
            }
            catch (ResourceNotFoundException) { }

            try
            {
                await _dbClient.CreateTableAsync(speciesRequest);
                await WaitUntilTableReadyAsync(speciesRequest.TableName);
                Console.WriteLine($"{speciesRequest.TableName} table created.");
            }
            catch (ResourceInUseException ex)
            {
                Console.WriteLine("Table already created.", ex.Message);
            }
        }

        public async Task<int> GetDatabaseCountAsync()
        {
            return await _speciesRepository.GetCountAsync();
        }

        public async Task LoadDataAsync(IEnumerable<SpeciesDynamoDb> speciesList)
        {
            var batchWrite = _dynamoDBContext.CreateBatchWrite<SpeciesDynamoDb>();
            batchWrite.AddPutItems(speciesList);
            await batchWrite.ExecuteAsync();
        }

        private CreateTableRequest PrepareTableCreation(string tableName, string keyName)
        {
            AttributeDefinition primaryKeyAttribute = new(keyName, ScalarAttributeType.N);

            KeySchemaElement primaryKey = new(keyName, KeyType.HASH);

            return new CreateTableRequest(tableName, new List<KeySchemaElement>() { primaryKey })
            {
                AttributeDefinitions = new List<AttributeDefinition>() { primaryKeyAttribute },
                BillingMode = BillingMode.PAY_PER_REQUEST
            };
        }

        private async Task WaitUntilTableDeletedAsync(string tableName)
        {
            bool firstTry = true;
            string status;
            do
            {
                if (!firstTry)
                {
                    Thread.Sleep(WAIT_TIMEOUT);
                }

                firstTry = false;
                var res = await _dbClient.DescribeTableAsync(new DescribeTableRequest
                {
                    TableName = tableName
                });

                status = res.Table.TableStatus;
            } while (status != String.Empty);
        }

        private async Task WaitUntilTableReadyAsync(string tableName)
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
                    var res = await _dbClient.DescribeTableAsync(new DescribeTableRequest
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
