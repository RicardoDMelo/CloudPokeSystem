using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PokemonSystem.Evolution.Application.Commands;
using PokemonSystem.Evolution.Domain.PokemonAggregate;
using System.Net;
using System.Text.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace PokemonSystem.Evolution.Application.Functions;
public class EvolutionFunction
{
    private readonly IMediator _mediator;

    public EvolutionFunction()
    {
        var serviceProvider = DependencyInjectionHelper.BuildServiceProvider();
        _mediator = serviceProvider.GetRequiredService<IMediator>();
    }

    /// <summary>
    /// A simple function that grants level to a pokemon
    /// </summary>
    /// <param name="grantPokemonLevel"></param>
    /// <returns>A pokemon</returns>
    [HttpPost(Name = "GrantPokemonLevel")]
    public async Task<APIGatewayProxyResponse> GrantPokemonLevelRestAsync(GrantPokemonLevel grantPokemonLevel)
    {
        var pokemon = await _mediator.Send(grantPokemonLevel);

        var response = new APIGatewayProxyResponse
        {
            StatusCode = (int)HttpStatusCode.OK,
            Body = JsonSerializer.Serialize(pokemon),
            Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
        };

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
        var list = new List<Pokemon>();

        foreach (var record in sqsEvent.Records)
        {
            var grantPokemonLevel = GrantPokemonLevel.FromString(record.Body);
            list.Add(await _mediator.Send(grantPokemonLevel));
        }

        return list;
    }
}