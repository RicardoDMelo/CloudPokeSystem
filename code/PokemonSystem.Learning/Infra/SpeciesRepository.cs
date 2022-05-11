using Amazon.DynamoDBv2.DataModel;
using PokemonSystem.Learning.Domain.SpeciesAggregate;
using PokemonSystem.Learning.Infra.Adapters;
using PokemonSystem.Learning.Infra.DatabaseDtos;

namespace PokemonSystem.Learning.Infra
{
    public class SpeciesRepository : ISpeciesRepository
    {
        private readonly IDynamoDBContext _dynamoDbContext;
        private readonly ISpeciesAdapter _speciesAdapter;

        public SpeciesRepository(IDynamoDBContext dynamoDbContext, ISpeciesAdapter speciesAdapter)
        {
            _dynamoDbContext = dynamoDbContext;
            _speciesAdapter = speciesAdapter;
        }

        public async Task<Species> GetAsync(uint id)
        {
            var speciesDto = await _dynamoDbContext.LoadAsync<SpeciesDynamoDb>(id);
            return _speciesAdapter.ConvertToModel(speciesDto);
        }
    }
}
