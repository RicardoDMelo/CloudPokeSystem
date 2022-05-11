using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using PokemonSystem.Incubator.Domain.SpeciesAggregate;
using PokemonSystem.Incubator.Infra.Adapters;
using PokemonSystem.Incubator.Infra.DatabaseDtos;
using PokemonSystem.Incubator.Infra.DataContracts;

namespace PokemonSystem.Incubator.Infra
{
    public class SpeciesRepository : IAppSpeciesRepository
    {
        private readonly IDynamoDBContext _dynamoDbContext;
        private readonly ISpeciesAdapter _speciesAdapter;

        public SpeciesRepository(IDynamoDBContext dynamoDbContext, ISpeciesAdapter speciesAdapter)
        {
            _dynamoDbContext = dynamoDbContext;
            _speciesAdapter = speciesAdapter;
        }

        public async Task<Species> GetRandomSpeciesAsync()
        {
            var databaseCount = await GetCountAsync();
            var randomPokemon = (uint)Random.Shared.Next(1, databaseCount + 1);
            var speciesDto = await _dynamoDbContext.LoadAsync<SpeciesDynamoDb>(randomPokemon);
            return _speciesAdapter.ConvertToModel(speciesDto);
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
