using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PokemonSystem.BillsPC.Application.Commands;
using System.Net;
using System.Text.Json;

namespace PokemonSystem.BillsPC.Application.Functions
{
    [Route("api/[controller]")]
    public class GetLastPokemonsFunction : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger<GetLastPokemonsFunction> _logger;

        public GetLastPokemonsFunction()
        {
            var serviceProvider = DependencyInjectionHelper.BuildServiceProvider();
            _logger = serviceProvider.GetRequiredService<ILogger<GetLastPokemonsFunction>>();
            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        /// <summary>
        /// A simple function that gets last pokemons
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A pokemon</returns>
        public async Task<APIGatewayProxyResponse> GetLastPokemonsRestAsync(APIGatewayProxyRequest request)
        {
            _logger.LogInformation("EVENT: " + JsonSerializer.Serialize(request, AppHelpers.SerializerOptions));

            APIGatewayProxyResponse response;

            var pokemons = await _mediator.Send(new GetLastPokemons());

            if (!pokemons.Any())
            {
                response = new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.NoContent,
                    Headers = AppHelpers.Headers
                };
            }
            else
            {

                response = new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Body = JsonSerializer.Serialize(pokemons, AppHelpers.SerializerOptions),
                    Headers = AppHelpers.Headers
                };
            }

            return response;
        }
    }
}
