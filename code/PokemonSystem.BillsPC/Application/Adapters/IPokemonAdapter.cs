using PokemonSystem.BillsPC.Application.ViewModel;
using PokemonSystem.BillsPC.Domain.PokemonAggregate;

namespace PokemonSystem.BillsPC.Application.Adapters
{
    public interface IPokemonAdapter
    {
        PokemonDetail ConvertToDetailViewModel(Pokemon pokemon);
        PokemonLookup ConvertToLookupViewModel(Pokemon pokemon);
    }
}
