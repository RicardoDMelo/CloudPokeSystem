using Amazon.Lambda.SQSEvents;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonSystem.Evolution.Application.Commands;
using PokemonSystem.Evolution.Domain.PokemonAggregate;

namespace PokemonSystem.Evolution.Application.Controllers
{
    [Route("api/[controller]")]
    public class EvolutionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EvolutionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// A simple function that grants level to a pokemon
        /// </summary>
        /// <param name="grantPokemonLevel"></param>
        /// <returns>A pokemon</returns>
        [HttpPost(Name = "GrantPokemonLevel")]
        public async Task<Pokemon> GrantPokemonLevelAsync([FromBody] GrantPokemonLevel grantPokemonLevel)
        {
            var pokemon = await _mediator.Send(grantPokemonLevel);
            return pokemon;
        }

        /// <summary>
        /// A simple function that grants level to multiple pokemon from a sqs event
        /// </summary>
        /// <param name="grantPokemonsLevel"></param>
        /// <returns>A pokemon</returns>
        [HttpPost("sqs", Name = "GrantPokemonsLevel")]
        public async Task<List<Pokemon>> GrantPokemonsLevelAsync([FromBody] SQSEvent sqsEvent)
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
}
