using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PokemonSystem.BillsPC.Application.Commands;
using System.Net;
using System.Text.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace PokemonSystem.BillsPC.Application.Functions
{
    [Route("api/[controller]")]
    public class GetPokemonFunction : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger<GetPokemonFunction> _logger;

        public GetPokemonFunction()
        {
            var serviceProvider = DependencyInjectionHelper.BuildServiceProvider();
            _logger = serviceProvider.GetRequiredService<ILogger<GetPokemonFunction>>();
            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        /// <summary>
        /// A simple function that gets a pokemon
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A pokemon</returns>
        public async Task<APIGatewayProxyResponse> GetPokemonRestAsync(APIGatewayProxyRequest request)
        {
            _logger.LogInformation("EVENT: " + JsonSerializer.Serialize(request));

            APIGatewayProxyResponse response;

            if (!Guid.TryParse(request.PathParameters["id"], out var getPokemonId))
            {
                response = new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "Invalid Id"
                };
            }
            else
            {
                var pokemon = await _mediator.Send(new GetPokemon(getPokemonId));

                if (pokemon == null)
                {
                    response = new APIGatewayProxyResponse
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                    };
                }
                else
                {

                    response = new APIGatewayProxyResponse
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Body = JsonSerializer.Serialize(pokemon),
                        Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                    };
                }
            }

            return response;
        }
    }
}
