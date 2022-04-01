using MediatR;
using PokemonSystem.Common.SeedWork;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Incubator.Application.Commands;
using PokemonSystem.Incubator.Domain;
using PokemonSystem.Incubator.Domain.PokemonAggregate;

namespace PokemonSystem.Incubator.Application.Handlers
{
    internal class CreateRandomPokemonHandler : IRequestHandler<CreateRandomPokemon, Pokemon>
    {
        private readonly IIncubatorService _incubatorService;
        private readonly IApplicationContext _applicationContext;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRandomPokemonHandler(IIncubatorService incubatorService, IApplicationContext applicationContext, IUnitOfWork unitOfWork)
        {
            _incubatorService = incubatorService ?? throw new ArgumentNullException(nameof(incubatorService));
            _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Pokemon> Handle(CreateRandomPokemon request, CancellationToken cancellationToken)
        {
            var pokemon = await _incubatorService.GenerateRandomPokemonAsync(request.Nickname, new Level(request.LevelToGrow));
            _applicationContext.Add(pokemon);
            _unitOfWork.Commit();
            return pokemon;
        }
    }
}
