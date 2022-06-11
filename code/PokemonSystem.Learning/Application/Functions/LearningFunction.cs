using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PokemonSystem.Learning.Application.Commands;
using PokemonSystem.Learning.Domain.PokemonAggregate;
using System.Net;
using System.Text.Json;
using static Amazon.Lambda.SNSEvents.SNSEvent;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace PokemonSystem.Learning.Application.Functions
{
    [Route("api/[controller]")]
    public class LearningFunction : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger<LearningFunction> _logger;
        private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        private readonly Dictionary<string, string> _headers = new Dictionary<string, string> {
                        {"Access-Control-Allow-Headers", "Content-Type" },
                        {"Access-Control-Allow-Origin", "*"},
                        {"Access-Control-Allow-Methods", "OPTIONS,POST"} };

        public LearningFunction()
        {
            var serviceProvider = DependencyInjectionHelper.BuildServiceProvider();
            _logger = serviceProvider.GetRequiredService<ILogger<LearningFunction>>();
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
            var teachPokemonMoves = JsonSerializer.Deserialize<TeachPokemonMoves>(request.Body, _serializerOptions);

            if (teachPokemonMoves == null)
            {
                response = new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "Invalid Json",
                    Headers = _headers
                };
            }
            else
            {
                var pokemon = await _mediator.Send(teachPokemonMoves);

                response = new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Body = JsonSerializer.Serialize(pokemon, _serializerOptions),
                    Headers = _headers
                };
            }

            return response;
        }

        /// <summary>
        /// A simple function that teachs moves to a pokemon
        /// </summary>
        /// <param name="sqsEvent"></param>
        /// <param name="context"></param>
        /// <returns>A pokemon list</returns>
        public async Task<List<Pokemon>> TeachPokemonMovesSQSAsync(SQSEvent sqsEvent, ILambdaContext context)
        {
            _logger.LogInformation("EVENT: " + JsonSerializer.Serialize(sqsEvent));

            var list = new List<Pokemon>();

            foreach (var record in sqsEvent.Records)
            {
                var snsMessage = JsonSerializer.Deserialize<SNSMessage>(record.Body);
                var teachPokemonMove = JsonSerializer.Deserialize<TeachPokemonMoves>(snsMessage!.Message);
                list.Add(await _mediator.Send(teachPokemonMove!));
            }

            return list;
        }
    }
}
