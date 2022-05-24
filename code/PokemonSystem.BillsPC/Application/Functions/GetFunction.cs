using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PokemonSystem.BillsPC.Application.Commands;
using PokemonSystem.BillsPC.Domain.PokemonAggregate;
using System.Net;
using System.Text.Json;
using static Amazon.Lambda.SNSEvents.SNSEvent;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace PokemonSystem.BillsPC.Application.Functions
{
    [Route("api/[controller]")]
    public class StorageFunction : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger<StorageFunction> _logger;

        public StorageFunction()
        {
            var serviceProvider = DependencyInjectionHelper.BuildServiceProvider();
            _logger = serviceProvider.GetRequiredService<ILogger<StorageFunction>>();
            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        /// <summary>
        /// A simple function that teachs moves to a pokemon
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A pokemon</returns>
        public async Task<APIGatewayProxyResponse> TeachPokemonMovesRestAsync(APIGatewayProxyRequest request)
        {
            _logger.LogInformation("EVENT: " + JsonSerializer.Serialize(request));

            APIGatewayProxyResponse response = null;

            return response;
        }
    }
}
