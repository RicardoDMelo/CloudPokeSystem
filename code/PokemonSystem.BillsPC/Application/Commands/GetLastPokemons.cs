using MediatR;
using PokemonSystem.BillsPC.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSystem.BillsPC.Application.Commands
{
    public class GetLastPokemons : IRequest<IEnumerable<PokemonLookup>>
    {
        public GetLastPokemons(){}        
    }
}
