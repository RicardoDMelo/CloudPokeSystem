namespace PokemonSystem.Learning.Domain.PokemonAggregate
{
    public interface IPokemonRepository
    {
        Task<Pokemon> GetAsync(Guid id);
        Task AddOrUpdateAsync(Pokemon pokemon);
    }
}
