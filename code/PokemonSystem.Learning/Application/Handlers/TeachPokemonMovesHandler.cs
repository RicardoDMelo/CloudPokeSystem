using MediatR;
using PokemonSystem.Common.SeedWork;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Learning.Application.Commands;
using PokemonSystem.Learning.Domain;
using PokemonSystem.Learning.Domain.PokemonAggregate;

namespace PokemonSystem.Learning.Application.Handlers
{
    public class TeachPokemonMovesHandler : IRequestHandler<TeachPokemonMoves, Pokemon>
    {
        private readonly ILearningService _learningService;
        private readonly IApplicationContext _applicationContext;
        private readonly IUnitOfWork _unitOfWork;

        public TeachPokemonMovesHandler(ILearningService learningService, IApplicationContext applicationContext, IUnitOfWork unitOfWork)
        {
            _learningService = learningService;
            _applicationContext = applicationContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<Pokemon> Handle(TeachPokemonMoves request, CancellationToken cancellationToken)
        {
            var pokemon = await _learningService.TeachRandomMovesAsync(request.Id, new Level(request.Level), request.SpeciesId);
            _applicationContext.Add(pokemon);
            _unitOfWork.Commit();
            return pokemon;
        }
    }
}
