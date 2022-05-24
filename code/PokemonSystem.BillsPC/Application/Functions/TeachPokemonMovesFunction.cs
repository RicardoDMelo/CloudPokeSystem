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
    public class TeachPokemonMovesFunction : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger<TeachPokemonMovesFunction> _logger;

        public TeachPokemonMovesFunction()
        {
            var serviceProvider = DependencyInjectionHelper.BuildServiceProvider();
            _logger = serviceProvider.GetRequiredService<ILogger<TeachPokemonMovesFunction>>();
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

            APIGatewayProxyResponse response;
            var teachPokemonMoves = JsonSerializer.Deserialize<TeachPokemonMoves>(request.Body);

            if (teachPokemonMoves == null)
            {
                response = new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "Invalid Json"
                };
            }
            else
            {
                await _mediator.Publish(teachPokemonMoves);

                response = new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };
            }

            return response;
        }

        /// <summary>
        /// A simple function that teachs moves to a pokemon
        /// </summary>
        /// <param name="sqsEvent"></param>
        /// <returns>A pokemon list</returns>
        public async Task TeachPokemonMovesSQSAsync(SQSEvent sqsEvent)
        {
            _logger.LogInformation("EVENT: " + JsonSerializer.Serialize(sqsEvent));


            foreach (var record in sqsEvent.Records)
            {
                var snsMessage = JsonSerializer.Deserialize<SNSMessage>(record.Body);
                var teachPokemonMove = JsonSerializer.Deserialize<TeachPokemonMoves>(snsMessage!.Message);
                await _mediator.Publish(teachPokemonMove!);
            }
        }
    }
}
