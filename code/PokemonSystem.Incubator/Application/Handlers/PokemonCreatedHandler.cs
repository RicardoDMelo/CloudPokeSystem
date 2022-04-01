using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using MediatR;
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
            _simpleNotificationService = simpleNotificationService ?? throw new ArgumentNullException(nameof(simpleNotificationService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(PokemonCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new PokemonCreatedIntegrationEvent(notification.Pokemon);
            var message = integrationEvent.ToString();
            var topicARN = _configuration.GetValue<string>("PokemonCreatedTopicARN");

            try
            {
                var publishRequest = new PublishRequest(topicARN, message);
                publishRequest.MessageGroupId = notification.Pokemon.Id.ToString();
                var result = await _simpleNotificationService.PublishAsync(publishRequest);
                _logger.LogInformation($"Pokemon Created: {notification.Pokemon.Nickname} | {notification.Pokemon.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error publishing pokemon {notification.Pokemon.Nickname} | {notification.Pokemon.Id}");
                throw;
            }
        }
    }
}
