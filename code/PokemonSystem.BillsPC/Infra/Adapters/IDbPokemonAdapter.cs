using MediatR;
using PokemonSystem.Common.SeedWork.Domain;

namespace PokemonSystem.BillsPC.Infra.Adapters
{
    public interface IDbPokemonAdapter
    {
        IEnumerable<EventRecordDb> ConvertToDto(Guid pokemonId, IEnumerable<INotification> notifications);
        IEnumerable<INotification> ConvertToModel(IEnumerable<EventRecordDb> pokemonsDynamoDb);
    }
}
