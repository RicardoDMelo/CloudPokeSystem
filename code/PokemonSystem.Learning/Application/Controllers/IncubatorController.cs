using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PokemonSystem.Learning.Application.Controllers
{
    [Route("api/[controller]")]
    public class IncubatorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IncubatorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        ///// <summary>
        ///// A simple function that generates a pokemon
        ///// </summary>
        ///// <param name="createRandomPokemon"></param>
        ///// <returns>A pokemon</returns>
        //[HttpPost(Name = "CreateRandomPokemon")]
        //public async Task CreateRandomPokemonAsync([FromBody] CreateRandomPokemon createRandomPokemon)
        //{
        //    var pokemon = await _mediator.Send(createRandomPokemon);
        //    return pokemon;
        //}
    }
}
