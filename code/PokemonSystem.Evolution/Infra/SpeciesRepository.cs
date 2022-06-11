using Amazon.DynamoDBv2.DataModel;
using PokemonSystem.Evolution.Infra.DatabaseDtos;
using PokemonSystem.Evolution.Infra.DataContracts;

namespace PokemonSystem.Evolution.Infra
{
    public class SpeciesRepository : ISpeciesRepository
    {
        private readonly IDynamoDBContext _dynamoDbContext;
        private readonly Random _random;

        public SpeciesRepository(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;
            _random = new Random();
        }

        public async Task<SpeciesDynamoDb> GetSpeciesAsync(uint id)
        {
            return await _dynamoDbContext.LoadAsync<SpeciesDynamoDb>(id);
        }
    }
}
