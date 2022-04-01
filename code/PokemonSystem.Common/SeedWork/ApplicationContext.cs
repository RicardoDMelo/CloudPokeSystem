using PokemonSystem.Common.SeedWork.Domain;

namespace PokemonSystem.Common.SeedWork
{
    public class ApplicationContext : IApplicationContext 
    {
        public ApplicationContext()
        {
            _entities = new List<IEntity>();
        }

        private readonly List<IEntity> _entities;
        public IReadOnlyCollection<IEntity> Entities => _entities.ToList().AsReadOnly();

        public void Add(IEntity entity)
        {
            _entities.Add(entity);
        }

        public void Reset()
        {
            _entities.Clear();
        }
    }
}
