namespace PokemonSystem.Learning.Domain.SpeciesAggregate
{
    public interface ISpeciesRepository
    {
        Task<Species> GetAsync(uint id);
    }
}
