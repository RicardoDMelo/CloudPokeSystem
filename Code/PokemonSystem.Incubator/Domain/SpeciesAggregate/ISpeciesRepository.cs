using PokemonSystem.Incubator.SpeciesAggregate;

namespace PokemonSystem.Incubator.Domain.SpeciesAggregate
{
    public interface ISpeciesRepository
    {
        Species GetRandomSpecies();
    }
}
