using PokemonSystem.Common.SeedWork.Domain;

namespace PokemonSystem.Common.SeedWork
{
    public interface IApplicationContext 
    {
        IReadOnlyCollection<IEntity> Entities { get; }
        void Add(IEntity entity);
        void Reset();
    }
}
