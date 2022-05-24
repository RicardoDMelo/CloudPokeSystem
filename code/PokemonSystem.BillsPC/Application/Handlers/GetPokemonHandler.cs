using MediatR;
using PokemonSystem.BillsPC.Application.Adapters;
using PokemonSystem.BillsPC.Application.Commands;
using PokemonSystem.BillsPC.Application.ViewModel;
using PokemonSystem.BillsPC.Domain.PokemonAggregate;

namespace PokemonSystem.BillsPC.Application.Handlers
{
    public class GetPokemonHandler : IRequestHandler<GetPokemon, PokemonDetail?>
    {
        private readonly IPokemonAdapter _pokemonAdapter;
        private readonly IPokemonRepository _pokemonRepository;

        public GetPokemonHandler(IPokemonAdapter pokemonAdapter, IPokemonRepository pokemonRepository)
        {
            _pokemonAdapter = pokemonAdapter;
            _pokemonRepository = pokemonRepository;
        }

        public async Task<PokemonDetail?> Handle(GetPokemon request, CancellationToken cancellationToken)
        {
            var pokemon = await _pokemonRepository.GetAsync(request.Id);
            if (pokemon is null) return null;
            var viewModel = _pokemonAdapter.ConvertToDetailViewModel(pokemon);
            return viewModel;
        }
    }
}
