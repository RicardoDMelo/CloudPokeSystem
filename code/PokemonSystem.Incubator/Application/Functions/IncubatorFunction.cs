using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PokemonSystem.Incubator.Application.Commands;
using System.Net;
using System.Text.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace PokemonSystem.Incubator.Application.Functions
{
    [Route("api/[controller]")]
    public class IncubatorFunction : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger<IncubatorFunction> _logger;

        public IncubatorFunction()
        {
            var serviceProvider = DependencyInjectionHelper.BuildServiceProvider();
            _logger = serviceProvider.GetRequiredService<ILogger<IncubatorFunction>>();
            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        /// <summary>
        /// A simple function that generates a pokemon
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A pokemon</returns>
        public async Task<APIGatewayProxyResponse> CreateRandomPokemonRestAsync(APIGatewayProxyRequest request)
        {
            _logger.LogInformation("EVENT: " + JsonSerializer.Serialize(request));

            APIGatewayProxyResponse response;
            var createRandomPokemon = JsonSerializer.Deserialize<CreateRandomPokemon>(request.Body);

            if (createRandomPokemon == null)
            {
                response = new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "Invalid Json"
                };
            }
            else
            {
                var pokemon = await _mediator.Send(createRandomPokemon);

                response = new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Body = JsonSerializer.Serialize(pokemon),
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };
            }

            return response;
        }
    }
}
