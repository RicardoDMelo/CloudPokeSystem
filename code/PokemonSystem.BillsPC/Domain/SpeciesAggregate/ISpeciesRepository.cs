namespace PokemonSystem.BillsPC.Domain.SpeciesAggregate
{
    public interface ISpeciesRepository
    {
        Task<Species> GetAsync(uint id);
    }
}
