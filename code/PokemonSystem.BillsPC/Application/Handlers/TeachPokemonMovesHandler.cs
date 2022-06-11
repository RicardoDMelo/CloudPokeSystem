using MediatR;
using PokemonSystem.BillsPC.Application.Commands;
using PokemonSystem.BillsPC.Domain.PokemonAggregate;
using PokemonSystem.Common.SeedWork;
using PokemonSystem.Common.ValueObjects;

namespace PokemonSystem.BillsPC.Application.Handlers
{
    public class TeachPokemonMovesHandler : INotificationHandler<TeachPokemonMoves>
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IApplicationContext _applicationContext;
        private readonly IUnitOfWork _unitOfWork;

        public TeachPokemonMovesHandler(IPokemonRepository pokemonRepository, IApplicationContext applicationContext, IUnitOfWork unitOfWork)
        {
            _pokemonRepository = pokemonRepository;
            _applicationContext = applicationContext;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(TeachPokemonMoves request, CancellationToken cancellationToken)
        {
            var pokemon = await _pokemonRepository.GetAsync(request.Id);
            pokemon!.Teach(request.Id, request.LearntMoves.Select(x => new Move(x.Name, x.Type, x.Category, x.Power, x.Accuracy, x.PP)).ToList());
            await _pokemonRepository.AddOrUpdateAsync(pokemon);
            _applicationContext.Add(pokemon);
            await _unitOfWork.CommitAsync();
        }
    }
}
