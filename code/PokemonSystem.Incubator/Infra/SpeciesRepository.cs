using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using PokemonSystem.Incubator.Infra.DatabaseDtos;
using PokemonSystem.Incubator.Infra.DataContracts;

namespace PokemonSystem.Incubator.Infra
{
    public class SpeciesRepository : IAppSpeciesRepository
    {
        private readonly IDynamoDBContext _dynamoDbContext;
        private readonly Random _random;

        public SpeciesRepository(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;
            _random = new Random();
        }

        public async Task<SpeciesDynamoDb> GetRandomSpeciesAsync()
        {
            var databaseCount = await GetCountAsync();
            var randomPokemon = (uint)_random.Next(1, databaseCount + 1);
            return await _dynamoDbContext.LoadAsync<SpeciesDynamoDb>(randomPokemon);
        }

        public async Task<int> GetCountAsync()
        {
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

                response = _dynamoDbContext.GetTargetTable<SpeciesDynamoDb>().Scan(queryOperation);

                await response.GetNextSetAsync();
                count += response.Count;
                paginationToken = response.PaginationToken;

            } while (!response.IsDone);

            return count;
        }
    }
}
