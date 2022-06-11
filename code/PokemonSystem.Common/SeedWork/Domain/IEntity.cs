using MediatR;

namespace PokemonSystem.Common.SeedWork.Domain
{
    public interface IEntity
    {
        List<INotification> DomainEvents { get; }
    }
}
