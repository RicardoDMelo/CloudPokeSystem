﻿using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PokemonSystem.Incubator.Application.IntegrationEvent;
using PokemonSystem.Incubator.Domain.PokemonAggregate;

namespace PokemonSystem.Incubator.Application.Handlers
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

        public async Task Handle(PokemonLearnedMovesDomainEvent notification, CancellationToken cancellationToken)
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
                var result = await _simpleNotificationService.PublishAsync(publishRequest);
                _logger.LogInformation($"Pokemon Learned Move: {notification.Pokemon.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error publishing pokemon learning move {notification.Pokemon.Id}");
                throw;
            }
        }
    }
}
