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
    public class RaisePokemonLevelFunction : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger<RaisePokemonLevelFunction> _logger;

        public RaisePokemonLevelFunction()
        {
            var serviceProvider = DependencyInjectionHelper.BuildServiceProvider();
            _logger = serviceProvider.GetRequiredService<ILogger<RaisePokemonLevelFunction>>();
            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        /// <summary>
        /// A simple function that raises a pokemon level
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A pokemon</returns>
        public async Task<APIGatewayProxyResponse> RaisePokemonLevelRestAsync(APIGatewayProxyRequest request)
        {
            _logger.LogInformation("EVENT: " + JsonSerializer.Serialize(request));

            APIGatewayProxyResponse response;
            var raisePokemonLevel = JsonSerializer.Deserialize<RaisePokemonLevel>(request.Body, AppHelpers.SerializerOptions);

            if (raisePokemonLevel == null)
            {
                response = new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = "Invalid Json",
                    Headers = AppHelpers.Headers
                };
            }
            else
            {
                await _mediator.Publish(raisePokemonLevel);

                response = new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Headers = AppHelpers.Headers
                };
            }

            return response;
        }

        /// <summary>
        /// A simple function that raises a pokemon level
        /// </summary>
        /// <param name="sqsEvent"></param>
        /// <returns>A pokemon list</returns>
        public async Task RaisePokemonLevelSQSAsync(SQSEvent sqsEvent)
        {
            _logger.LogInformation("EVENT: " + JsonSerializer.Serialize(sqsEvent));


            foreach (var record in sqsEvent.Records)
            {
                var snsMessage = JsonSerializer.Deserialize<SNSMessage>(record.Body);
                var raisePokemonLevel = JsonSerializer.Deserialize<RaisePokemonLevel>(snsMessage!.Message);
                await _mediator.Publish(raisePokemonLevel!);
            }
        }
    }
}
