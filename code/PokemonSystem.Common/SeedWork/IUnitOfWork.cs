namespace PokemonSystem.Common.SeedWork
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
