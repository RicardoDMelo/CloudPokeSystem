using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.SQSEvents;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PokemonSystem.BillsPC.Application.Commands;
using System.Net;
using System.Text.Json;
using static Amazon.Lambda.SNSEvents.SNSEvent;

namespace PokemonSystem.BillsPC.Application.Functions
{
    [Route("api/[controller]")]
    public class EvolvePokemonFunction : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger<EvolvePokemonFunction> _logger;

        public EvolvePokemonFunction()
        {
            var serviceProvider = DependencyInjectionHelper.BuildServiceProvider();
            _logger = serviceProvider.GetRequiredService<ILogger<EvolvePokemonFunction>>();
            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        /// <summary>
        /// A simple function that evolves a pokemon
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A pokemon</returns>
        public async Task<APIGatewayProxyResponse> EvolvePokemonRestAsync(APIGatewayProxyRequest request)
        {
            _logger.LogInformation("EVENT: " + JsonSerializer.Serialize(request));

            APIGatewayProxyResponse response;
            var evolvePokemon = JsonSerializer.Deserialize<EvolvePokemon>(request.Body);

            if (evolvePokemon == null)
            {
                response = new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "Invalid Json"
                };
            }
            else
            {
               await _mediator.Publish(evolvePokemon);

                response = new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };
            }

            return response;
        }

        /// <summary>
        /// A simple function that evolves a pokemon
        /// </summary>
        /// <param name="sqsEvent"></param>
        /// <returns>A pokemon list</returns>
        public async Task EvolvePokemonSQSAsync(SQSEvent sqsEvent)
        {
            _logger.LogInformation("EVENT: " + JsonSerializer.Serialize(sqsEvent));

            foreach (var record in sqsEvent.Records)
            {
                var snsMessage = JsonSerializer.Deserialize<SNSMessage>(record.Body);
                var evolvePokemon = JsonSerializer.Deserialize<EvolvePokemon>(snsMessage!.Message);
                await _mediator.Publish(evolvePokemon!);
            }
        }
    }
}
