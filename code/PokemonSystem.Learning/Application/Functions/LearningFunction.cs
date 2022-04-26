using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PokemonSystem.Evolution.Application.Commands;
using PokemonSystem.Learning.Domain.PokemonAggregate;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace PokemonSystem.Incubator.Application.Functions
{
    [Route("api/[controller]")]
    public class LearningFunction : ControllerBase
    {

        private readonly IMediator _mediator;

        public LearningFunction()
        {
            var serviceProvider = DependencyInjectionHelper.BuildServiceProvider();
            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        /// <summary>
        /// A simple function that teachs moves to a pokemon
        /// </summary>
        /// <param name="teachPokemonLevel"></param>
        /// <returns>A pokemon</returns>
        public async Task<Pokemon> TeachPokemonMovesRestAsync(TeachPokemonMoves teachPokemonMoves)
        {
            return await _mediator.Send(teachPokemonMoves);
        }

        /// <summary>
        /// A simple function that teachs moves to a pokemon
        /// </summary>
        /// <param name="sqsEvent"></param>
        /// <param name="context"></param>
        /// <returns>A pokemon list</returns>
        public async Task<List<Pokemon>> TeachPokemonMovesSQSAsync(SQSEvent sqsEvent, ILambdaContext context)
        {
            var list = new List<Pokemon>();

            foreach (var record in sqsEvent.Records)
            {
                var teachPokemonMoves = TeachPokemonMoves.FromString(record.Body);
                list.Add(await _mediator.Send(teachPokemonMoves));
            }

            return list;
        }
    }
}
