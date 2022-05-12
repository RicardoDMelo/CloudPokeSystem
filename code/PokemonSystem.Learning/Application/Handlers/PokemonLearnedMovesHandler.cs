using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PokemonSystem.Learning.Application.IntegrationEvent;
using PokemonSystem.Learning.Domain.PokemonAggregate;

namespace PokemonSystem.Learning.Application.Handlers
{
    public class PokemonLearnedMovesHandler : INotificationHandler<PokemonLearnedMovesDomainEvent>
    {
        private readonly IAmazonSimpleNotificationService _simpleNotificationService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PokemonLearnedMovesHandler> _logger;

        public PokemonLearnedMovesHandler(IAmazonSimpleNotificationService simpleNotificationService, IConfiguration configuration, ILogger<PokemonLearnedMovesHandler> logger)
        {
            _simpleNotificationService = simpleNotificationService;
            _configuration = configuration;
            _logger = logger;
        }

        public Task Handle(PokemonLearnedMovesDomainEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new PokemonLearnedMovesIntegrationEvent(notification.Pokemon);
            var message = integrationEvent.ToString();
            var topicARN = _configuration.GetValue<string>("PokemonLearnedMoveTopicARN");

            try
            {
                var publishRequest = new PublishRequest(topicARN, message)
                {
                    MessageGroupId = notification.Pokemon.Id.ToString()
                };
                _ = _simpleNotificationService.PublishAsync(publishRequest, cancellationToken);
                _logger.LogInformation($"Pokemon Learned Move: {notification.Pokemon.Id}");
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error publishing pokemon learning move {notification.Pokemon.Id}");
                throw;
            }
        }
    }
}
