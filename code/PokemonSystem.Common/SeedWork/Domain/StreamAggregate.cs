namespace PokemonSystem.Common.SeedWork.Domain
{
    public abstract class StreamAggregate<T> : Entity<T>, IAggregateRoot
    {
        public StreamAggregate(T id) : base(id) { }
    }
}
