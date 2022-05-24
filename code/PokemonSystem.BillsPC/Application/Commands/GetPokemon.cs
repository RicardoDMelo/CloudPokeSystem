using MediatR;
using PokemonSystem.BillsPC.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonSystem.BillsPC.Application.Commands
{
    public class GetPokemon : IRequest<PokemonDetail?>
    {
        public GetPokemon(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
