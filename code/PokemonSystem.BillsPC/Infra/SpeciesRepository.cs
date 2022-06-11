using Amazon.DynamoDBv2.DataModel;
using PokemonSystem.BillsPC.Domain.SpeciesAggregate;
using PokemonSystem.BillsPC.Infra.Adapters;
using PokemonSystem.BillsPC.Infra.DatabaseDtos;

namespace PokemonSystem.BillsPC.Infra
{
    public class SpeciesRepository : ISpeciesRepository
    {
        private readonly IDynamoDBContext _dynamoDbContext;
        private readonly IDbSpeciesAdapter _speciesAdapter;

        public SpeciesRepository(IDynamoDBContext dynamoDbContext, IDbSpeciesAdapter speciesAdapter)
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
