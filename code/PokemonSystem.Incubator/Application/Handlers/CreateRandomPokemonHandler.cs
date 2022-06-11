using MediatR;
using PokemonSystem.Common.SeedWork;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Incubator.Application.Adapters;
using PokemonSystem.Incubator.Application.Commands;
using PokemonSystem.Incubator.Application.ViewModel;
using PokemonSystem.Incubator.Domain;

namespace PokemonSystem.Incubator.Application.Handlers
{
    public class CreateRandomPokemonHandler : IRequestHandler<CreateRandomPokemon, PokemonLookup>
    {
        private readonly IIncubatorService _incubatorService;
        private readonly IApplicationContext _applicationContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPokemonAdapter _pokemonAdapter;

        public CreateRandomPokemonHandler(IIncubatorService incubatorService, IApplicationContext applicationContext, 
            IUnitOfWork unitOfWork, IPokemonAdapter pokemonAdapter)
        {
            _incubatorService = incubatorService;
            _applicationContext = applicationContext;
            _unitOfWork = unitOfWork;
            _pokemonAdapter = pokemonAdapter;
        }

        public async Task<PokemonLookup> Handle(CreateRandomPokemon request, CancellationToken cancellationToken)
        {
            var pokemon = await _incubatorService.GenerateRandomPokemonAsync(request.Nickname, new Level(request.LevelToGrow));
            _applicationContext.Add(pokemon);
            await _unitOfWork.CommitAsync();
            var viewModel = _pokemonAdapter.ConvertToLookupViewModel(pokemon);
            return viewModel;
        }
    }
}
