using Amazon.SimpleNotificationService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using PokemonSystem.Evolution.Application.Handlers;
using PokemonSystem.Incubator.Application.Handlers;
using PokemonSystem.Incubator.Domain.PokemonAggregate;
using PokemonSystem.Tests.Incubator.Builders;

namespace PokemonSystem.Tests.Incubator.Handlers
{
    public class PokemonCreatedHandlerTests
    {
        private IAmazonSimpleNotificationService? _simpleNotificationService;
        private IConfiguration? _configuration;
        private ILogger<PokemonCreatedHandler>? _logger;

        private PokemonBuilder? _pokemonBuilder;
        private PokemonCreatedHandler? _handler;

        [SetUp]
        public void Setup()
        {
            _simpleNotificationService = Substitute.For<IAmazonSimpleNotificationService>();
            _configuration = Substitute.For<IConfiguration>();
            _logger = Substitute.For<ILogger<PokemonCreatedHandler>>();
            _pokemonBuilder = new PokemonBuilder();

            _handler = new PokemonCreatedHandler(_simpleNotificationService, _configuration, _logger);
        }

        [Test]
        public async Task Handle_Command()
        {
            var pokemon = _pokemonBuilder!.Build();
            var command = new PokemonCreatedDomainEvent(pokemon);
            var token = new CancellationTokenSource().Token;

            await _handler!.Handle(command, token);

            await _simpleNotificationService!.ReceivedWithAnyArgs(1).PublishAsync(default);
            _logger!.ReceivedWithAnyArgs(1).LogInformation(default);
        }
    }
}