using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using MediatR;
using PokemonSystem.BillsPC.Domain.PokemonAggregate;
using PokemonSystem.BillsPC.Infra.Adapters;
using PokemonSystem.BillsPC.Infra.DatabaseDtos;

namespace PokemonSystem.BillsPC.Infra
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly IDynamoDBContext _dynamoDbContext;
        private readonly IDbPokemonAdapter _pokemonAdapter;
        private const int POKEMON_HISTORY_COUNT = 10;

        public PokemonRepository(IDynamoDBContext dynamoDbContext, IDbPokemonAdapter pokemonAdapter)
        {
            _dynamoDbContext = dynamoDbContext;
            _pokemonAdapter = pokemonAdapter;
        }

        public async Task AddOrUpdateAsync(Pokemon pokemon)
        {
            var eventRecordsDb = _pokemonAdapter.ConvertToDto(pokemon.Id, pokemon.DomainEvents);
            var batchWrite = _dynamoDbContext.CreateBatchWrite<EventRecordDb>();

            batchWrite.AddPutItems(eventRecordsDb);
            await batchWrite.ExecuteAsync();

            var pokemons = await GetHistoriesAsync();
            if (pokemons.Count() >= POKEMON_HISTORY_COUNT)
            {
                var lastPokemons = pokemons.Take(POKEMON_HISTORY_COUNT - 1);
                var pokemonsToRemove = pokemons.Except(lastPokemons);
                foreach(var pokemonToRemove in pokemonsToRemove)
                {
                    await _dynamoDbContext.DeleteAsync(pokemonToRemove);
                }
            }

            var historyDb = _pokemonAdapter.ConvertToDto(pokemon);
            await _dynamoDbContext.SaveAsync(historyDb);
        }

        public async Task<Pokemon?> GetAsync(Guid id)
        {
            var query = new QueryOperationConfig();
            query.KeyExpression = new Expression();
            query.KeyExpression.ExpressionStatement = "StreamId = :streamId";
            query.KeyExpression.ExpressionAttributeValues.Add(":streamId", id);

            var asyncSearch = _dynamoDbContext.FromQueryAsync<EventRecordDb>(query);
            var eventRecords = new List<INotification>();

            do
            {
                var eventRecordsDb = await asyncSearch.GetNextSetAsync();
                eventRecords.AddRange(_pokemonAdapter.ConvertToModel(eventRecordsDb));
            } while (!asyncSearch.IsDone);

            if (!eventRecords.Any()) return null;
            return new Pokemon(id, eventRecords);
        }

        public async Task<IEnumerable<Pokemon>> GetLastPokemonsAsync()
        {
            var historyDbs = await GetHistoriesAsync();
            var list = new List<Pokemon>();

            foreach (var historyDb in historyDbs)
            {
                var pokemon = await GetAsync(Guid.Parse(historyDb.Id));
                if (pokemon is not null)
                {
                    list.Add(pokemon);
                }
            }

            return list;
        }

        private async Task<IEnumerable<PokemonHistoryDb>> GetHistoriesAsync()
        {
            AsyncSearch<PokemonHistoryDb> response;
            var historyDbs = new List<PokemonHistoryDb>();

            response = _dynamoDbContext.FromScanAsync<PokemonHistoryDb>(new ScanOperationConfig());

            do
            {
                var historyDb = await response.GetNextSetAsync();
                historyDbs.AddRange(historyDb);
            } while (!response.IsDone);

            return historyDbs.OrderByDescending(x => x.Timestamp);
        }
    }
}
