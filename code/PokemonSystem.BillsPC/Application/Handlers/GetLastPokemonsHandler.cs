using MediatR;
using PokemonSystem.BillsPC.Application.Adapters;
using PokemonSystem.BillsPC.Application.Commands;
using PokemonSystem.BillsPC.Application.ViewModel;
using PokemonSystem.BillsPC.Domain.PokemonAggregate;

namespace PokemonSystem.BillsPC.Application.Handlers
{
    public class GetLastPokemonsHandler : IRequestHandler<GetLastPokemons, IEnumerable<PokemonLookup>>
    {
        private readonly IPokemonAdapter _pokemonAdapter;
        private readonly IPokemonRepository _pokemonRepository;

        public GetLastPokemonsHandler(IPokemonAdapter pokemonAdapter, IPokemonRepository pokemonRepository)
        {
            _pokemonAdapter = pokemonAdapter;
            _pokemonRepository = pokemonRepository;
        }

        public async Task<IEnumerable<PokemonLookup>> Handle(GetLastPokemons request, CancellationToken cancellationToken)
        {
            var pokemons = await _pokemonRepository.GetLastPokemonsAsync();
            var viewModels = pokemons.Select(_pokemonAdapter.ConvertToLookupViewModel);
            return viewModels;
        }
    }
}
