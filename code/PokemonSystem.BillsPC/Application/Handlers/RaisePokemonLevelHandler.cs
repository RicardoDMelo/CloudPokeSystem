using MediatR;
using PokemonSystem.BillsPC.Application.Commands;
using PokemonSystem.BillsPC.Domain.PokemonAggregate;
using PokemonSystem.Common.SeedWork;
using PokemonSystem.Common.ValueObjects;

namespace PokemonSystem.BillsPC.Application.Handlers
{
    public class RaisePokemonLevelHandler : INotificationHandler<RaisePokemonLevel>
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IApplicationContext _applicationContext;
        private readonly IUnitOfWork _unitOfWork;

        public RaisePokemonLevelHandler(IPokemonRepository pokemonRepository, IApplicationContext applicationContext, IUnitOfWork unitOfWork)
        {
            _pokemonRepository = pokemonRepository;
            _applicationContext = applicationContext;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RaisePokemonLevel request, CancellationToken cancellationToken)
        {
            var pokemon = await _pokemonRepository.GetAsync(request.Id);
            pokemon!.RaiseLevel(request.Id, new Level(request.Level), request.Stats);
            await _pokemonRepository.AddOrUpdateAsync(pokemon);
            _applicationContext.Add(pokemon);
            await _unitOfWork.CommitAsync();
        }
    }
}
