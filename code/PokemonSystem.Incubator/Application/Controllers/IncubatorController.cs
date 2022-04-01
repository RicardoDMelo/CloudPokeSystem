using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonSystem.Incubator.Application.Commands;
using PokemonSystem.Incubator.Domain.PokemonAggregate;

namespace PokemonSystem.Incubator.Application.Controllers
{
    [Route("api/[controller]")]
    public class IncubatorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IncubatorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// A simple function that generates a pokemon
        /// </summary>
        /// <param name="createRandomPokemon"></param>
        /// <returns>A pokemon</returns>
        [HttpPost(Name = "CreateRandomPokemon")]
        public async Task<Pokemon> CreateRandomPokemonAsync([FromBody] CreateRandomPokemon createRandomPokemon)
        {
            var pokemon = await _mediator.Send(createRandomPokemon);
            return pokemon;
        }
    }
}
