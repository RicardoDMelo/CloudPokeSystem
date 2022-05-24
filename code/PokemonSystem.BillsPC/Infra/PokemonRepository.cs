using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using MediatR;
using PokemonSystem.BillsPC.Domain.PokemonAggregate;
using PokemonSystem.BillsPC.Infra.Adapters;
using PokemonSystem.Common.SeedWork.Domain;

namespace PokemonSystem.BillsPC.Infra
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly IDynamoDBContext _dynamoDbContext;
        private readonly IDbPokemonAdapter _pokemonAdapter;

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
        }

        public async Task<Pokemon?> GetAsync(Guid id)
        {
            var query = new QueryOperationConfig();
            query.KeyExpression = new Expression();
            query.KeyExpression.ExpressionStatement = "StreamId = :streamId";
            query.KeyExpression.ExpressionAttributeValues.Add(":streamId", id);

            var asyncSearch = _dynamoDbContext.FromQueryAsync<EventRecordDb>(query);
            var eventRecords = new List<INotification>();

            while (!asyncSearch.IsDone)
            {
                var eventRecordsDb = await asyncSearch.GetNextSetAsync();
                eventRecords.AddRange(_pokemonAdapter.ConvertToModel(eventRecordsDb));
            }

            if (!eventRecords.Any()) return null;
            return new Pokemon(id, eventRecords);
        }
    }
}
