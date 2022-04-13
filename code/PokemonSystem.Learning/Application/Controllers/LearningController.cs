using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PokemonSystem.Learning.Application.Controllers
{
    [Route("api/[controller]")]
    public class LearningController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LearningController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// A simple function that generates a pokemon
        /// </summary>
        /// <param name="createRandomPokemon"></param>
        /// <returns>A pokemon</returns>
        [HttpPost(Name = "CreateRandomPokemon")]
        public Task TeachPokemonMovesAsync()
        {
            return Task.CompletedTask;
        }
    }
}
