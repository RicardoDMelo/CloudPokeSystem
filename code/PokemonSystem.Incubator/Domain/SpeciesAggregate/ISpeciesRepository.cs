using PokemonSystem.Incubator.Infra.DatabaseDtos;

namespace PokemonSystem.Incubator.Domain.SpeciesAggregate
{
    public interface ISpeciesRepository
    {
        Task<Species> GetRandomSpeciesAsync();
    }
}
