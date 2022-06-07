using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PokemonSystem.Evolution.Application.Commands;
using PokemonSystem.Evolution.Domain.PokemonAggregate;
using System.Net;
using System.Text.Json;
using static Amazon.Lambda.SNSEvents.SNSEvent;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace PokemonSystem.Evolution.Application.Functions;
public class EvolutionFunction
{
    private readonly IMediator _mediator;
    private readonly ILogger<EvolutionFunction> _logger;
    private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };
    private readonly Dictionary<string, string> _headers = new Dictionary<string, string> {
                        {"Access-Control-Allow-Headers", "Content-Type" },
                        {"Access-Control-Allow-Origin", "*"},
                        {"Access-Control-Allow-Methods", "OPTIONS,POST"} };

    public EvolutionFunction()
    {
        var serviceProvider = DependencyInjectionHelper.BuildServiceProvider();
        _logger = serviceProvider.GetRequiredService<ILogger<EvolutionFunction>>();
        _mediator = serviceProvider.GetRequiredService<IMediator>();
    }

    /// <summary>
    /// A simple function that grants level to a pokemon
    /// </summary>
    /// <param name="request"></param>
    /// <returns>A pokemon</returns>
    public async Task<APIGatewayProxyResponse> GrantPokemonLevelRestAsync(APIGatewayProxyRequest request)
    {
        _logger.LogInformation("EVENT: " + JsonSerializer.Serialize(request));

        APIGatewayProxyResponse response;
        var grantPokemonLevel = JsonSerializer.Deserialize<GrantPokemonLevel>(request.Body, _serializerOptions);

        if (grantPokemonLevel == null)
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
            var pokemon = await _mediator.Send(grantPokemonLevel);

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
    /// A simple function that grants level to multiple pokemon from a sqs event
    /// </summary>
    /// <param name="sqsEvent"></param>
    /// <param name="context"></param>
    /// <returns>A pokemon list</returns>
    public async Task<List<Pokemon>> GrantPokemonLevelSQSAsync(SQSEvent sqsEvent, ILambdaContext context)
    {
        _logger.LogInformation("EVENT: " + JsonSerializer.Serialize(sqsEvent));

        var list = new List<Pokemon>();
        foreach (var record in sqsEvent.Records)
        {
            var snsMessage = JsonSerializer.Deserialize<SNSMessage>(record.Body);
            var grantPokemonLevel = JsonSerializer.Deserialize<GrantPokemonLevel>(snsMessage!.Message);
            list.Add(await _mediator.Send(grantPokemonLevel!));
        }

        return list;
    }
}