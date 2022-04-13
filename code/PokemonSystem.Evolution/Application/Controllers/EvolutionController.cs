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
    }
}
