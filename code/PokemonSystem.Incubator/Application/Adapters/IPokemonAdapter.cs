using PokemonSystem.Incubator.Application.ViewModel;
using PokemonSystem.Incubator.Domain.PokemonAggregate;

namespace PokemonSystem.Incubator.Application.Adapters
{
    public interface IPokemonAdapter
    {
        PokemonLookup ConvertToLookupViewModel(Pokemon pokemon);
    }
}
