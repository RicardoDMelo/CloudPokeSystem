using NSubstitute;
using NUnit.Framework;
using PokemonSystem.Common.Enums;
using PokemonSystem.Common.SeedWork;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Incubator.Application.Commands;
using PokemonSystem.Incubator.Application.Handlers;
using PokemonSystem.Incubator.Domain;
using PokemonSystem.Incubator.Domain.SpeciesAggregate;
using PokemonSystem.Tests.Incubator.Builders;
using PokemonSystem.Tests.ValueObjects;
using System;

namespace PokemonSystem.Tests.Incubator.Handlers
{
    public class CreateRandomPokemonHandlerTests
    {
        private IIncubatorService? _incubatorService;
        private IApplicationContext? _applicationContext;
        private IUnitOfWork? _unitOfWork;

        private PokemonBuilder? _pokemonBuilder;
        private CreateRandomPokemonHandler? _handler;

        [SetUp]
        public void Setup()
        {
            _incubatorService = Substitute.For<IIncubatorService>();
            _applicationContext = Substitute.For<IApplicationContext>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _pokemonBuilder = new PokemonBuilder();

            _handler = new CreateRandomPokemonHandler(_incubatorService, _applicationContext, _unitOfWork);
        }

        [Test]
        public async Task Handle_Command()
        {
            var command = new CreateRandomPokemon();
            var token = new CancellationTokenSource().Token;
            var pokemon = _pokemonBuilder!.Build();

            _incubatorService!.GenerateRandomPokemonAsync(Arg.Any<string>(), Arg.Any<Level>()).Returns(pokemon);

            var result = await _handler!.Handle(command, token);
            _applicationContext!.Received(1).Add(Arg.Is(pokemon));

            Assert.IsNotNull(result);
            Assert.AreEqual(pokemon, result);
        }
    }
}