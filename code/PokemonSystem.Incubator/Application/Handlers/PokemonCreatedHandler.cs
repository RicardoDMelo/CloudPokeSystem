using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PokemonSystem.Incubator.Application.IntegrationEvent;
using PokemonSystem.Incubator.Domain.PokemonAggregate;

namespace PokemonSystem.Incubator.Application.Handlers
{
    public class PokemonCreatedHandler : INotificationHandler<PokemonCreatedDomainEvent>
    {
        private readonly IAmazonSimpleNotificationService _simpleNotificationService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PokemonCreatedHandler> _logger;

        public PokemonCreatedHandler(IAmazonSimpleNotificationService simpleNotificationService, IConfiguration configuration, ILogger<PokemonCreatedHandler> logger)
        {
            _simpleNotificationService = simpleNotificationService;
            _configuration = configuration;
            _logger = logger;
        }

        public Task Handle(PokemonCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new PokemonCreatedIntegrationEvent(notification.Pokemon);
            var message = integrationEvent.ToString();
            var topicARN = _configuration.GetValue<string>("PokemonCreatedTopicARN");

            try
            {
                var publishRequest = new PublishRequest(topicARN, message)
                {
                    MessageGroupId = notification.Pokemon.Id.ToString()
                };
                _ = _simpleNotificationService.PublishAsync(publishRequest, cancellationToken);
                _logger.LogInformation($"Pokemon Created: {notification.Pokemon.Nickname} | {notification.Pokemon.Id}");
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error publishing pokemon {notification.Pokemon.Nickname} | {notification.Pokemon.Id}");
                throw;
            }
        }
    }
}
