namespace PokemonSystem.BillsPC.Domain.PokemonAggregate
{
    public interface IPokemonRepository
    {
        Task<Pokemon?> GetAsync(Guid id);
        Task AddOrUpdateAsync(Pokemon pokemon);
        Task<IEnumerable<Pokemon>> GetLastPokemonsAsync();
    }
}
