using Amazon.DynamoDBv2.DataModel;
using PokemonSystem.Learning.Domain.PokemonAggregate;
using PokemonSystem.Learning.Domain.SpeciesAggregate;
using PokemonSystem.Learning.Infra.Adapters;
using PokemonSystem.Learning.Infra.DatabaseDtos;

namespace PokemonSystem.Learning.Infra
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly IDynamoDBContext _dynamoDbContext;
        private readonly IPokemonAdapter _pokemonAdapter;

        public PokemonRepository(IDynamoDBContext dynamoDbContext, IPokemonAdapter pokemonAdapter)
        {
            _dynamoDbContext = dynamoDbContext;
            _pokemonAdapter = pokemonAdapter;
        }

        public async Task AddOrUpdateAsync(Pokemon pokemon)
        {
            var pokemonDto = _pokemonAdapter.ConvertToDto(pokemon);
            await _dynamoDbContext.SaveAsync(pokemonDto);
        }

        public async Task<Pokemon> GetAsync(Guid id)
        {
            var pokemonDto = await _dynamoDbContext.LoadAsync<PokemonDynamoDb>(id.ToString());
            return _pokemonAdapter.ConvertToModel(pokemonDto);
        }
    }
}
