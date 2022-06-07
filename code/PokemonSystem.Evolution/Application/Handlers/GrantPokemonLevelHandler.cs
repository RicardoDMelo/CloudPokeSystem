using MediatR;
using PokemonSystem.Common.SeedWork;
using PokemonSystem.Evolution.Application.Adapters;
using PokemonSystem.Evolution.Application.Commands;
using PokemonSystem.Evolution.Domain.PokemonAggregate;
using PokemonSystem.Evolution.Infra.DataContracts;

namespace PokemonSystem.Evolution.Application.Handlers
{
    public class GrantPokemonLevelHandler : IRequestHandler<GrantPokemonLevel, Pokemon>
    {
        private readonly IPokemonAdapter _pokemonAdapter;
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IApplicationContext _applicationContext;
        private readonly IUnitOfWork _unitOfWork;

        public GrantPokemonLevelHandler(IPokemonAdapter pokemonAdapter, ISpeciesRepository speciesRepository, 
            IApplicationContext applicationContext, IUnitOfWork unitOfWork)
        {
            _pokemonAdapter = pokemonAdapter;
            _speciesRepository = speciesRepository;
            _applicationContext = applicationContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<Pokemon> Handle(GrantPokemonLevel request, CancellationToken cancellationToken)
        {
            var species = await _speciesRepository.GetSpeciesAsync(request.SpeciesId);
            var pokemon = _pokemonAdapter.ConvertToModel(request, species);
            _applicationContext.Add(pokemon);
            await _unitOfWork.CommitAsync();
            return pokemon;
        }
    }
}
