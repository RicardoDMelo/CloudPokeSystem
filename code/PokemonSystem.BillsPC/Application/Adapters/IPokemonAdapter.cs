using PokemonSystem.BillsPC.Application.ViewModel;
using PokemonSystem.BillsPC.Domain.PokemonAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSystem.BillsPC.Application.Adapters
{
    public interface IPokemonAdapter
    {
        PokemonDetail ConvertToDetailViewModel(Pokemon pokemon);
        PokemonLookup ConvertToLookupViewModel(Pokemon pokemon);
    }
}
