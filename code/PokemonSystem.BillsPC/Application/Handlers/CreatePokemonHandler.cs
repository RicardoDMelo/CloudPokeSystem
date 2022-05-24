using MediatR;
using PokemonSystem.BillsPC.Application.Commands;
using PokemonSystem.BillsPC.Domain.PokemonAggregate;
using PokemonSystem.BillsPC.Domain.SpeciesAggregate;
using PokemonSystem.Common.SeedWork;

namespace PokemonSystem.BillsPC.Application.Handlers
{
    public class CreatePokemonHandler : INotificationHandler<CreatePokemon>
    {
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IApplicationContext _applicationContext;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePokemonHandler(ISpeciesRepository speciesRepository, IApplicationContext applicationContext,
            IPokemonRepository pokemonRepository, IUnitOfWork unitOfWork)
        {
            _speciesRepository = speciesRepository;
            _applicationContext = applicationContext;
            _pokemonRepository = pokemonRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreatePokemon request, CancellationToken cancellationToken)
        {
            var species = await _speciesRepository.GetAsync(request.SpeciesId);
            var pokemon = new Pokemon(request.Id, request.Nickname, species, request.Gender);
            await _pokemonRepository.AddOrUpdateAsync(pokemon);
            _applicationContext.Add(pokemon);
            _unitOfWork.Commit();
        }
    }
}
