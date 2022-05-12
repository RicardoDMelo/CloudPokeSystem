using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PokemonSystem.Evolution.Application.IntegrationEvent;
using PokemonSystem.Evolution.Domain.PokemonAggregate;

namespace PokemonSystem.Evolution.Application.Handlers
{
    public class PokemonLevelRaisedHandler : INotificationHandler<PokemonLevelRaisedDomainEvent>
    {
        private readonly IAmazonSimpleNotificationService _simpleNotificationService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PokemonEvolvedHandler> _logger;

        public PokemonLevelRaisedHandler(IAmazonSimpleNotificationService simpleNotificationService, IConfiguration configuration, ILogger<PokemonEvolvedHandler> logger)
        {
            _simpleNotificationService = simpleNotificationService;
            _configuration = configuration;
            _logger = logger;
        }

        public Task Handle(PokemonLevelRaisedDomainEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new PokemonLevelRaisedIntegrationEvent(notification.Pokemon);
            var message = integrationEvent.ToString();
            var topicARN = _configuration.GetValue<string>("PokemonLevelRaisedTopicARN");

            try
            {
                var publishRequest = new PublishRequest(topicARN, message)
                {
                    MessageGroupId = notification.Pokemon.Id.ToString()
                };
                _= _simpleNotificationService.PublishAsync(publishRequest, cancellationToken);
                _logger.LogInformation($"Pokemon Level Raised: {notification.Pokemon.Id}");
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error publishing pokemon level {notification.Pokemon.Id}");
                throw;
            }
        }
    }
}
