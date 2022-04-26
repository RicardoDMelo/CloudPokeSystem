using Amazon.Lambda.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PokemonSystem.Incubator.Application.Commands;
using PokemonSystem.Incubator.Domain.PokemonAggregate;


[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace PokemonSystem.Incubator.Application.Functions
{
    [Route("api/[controller]")]
    public class IncubatorFunction : ControllerBase
    {

        private readonly IMediator _mediator;

        public IncubatorFunction()
        {
            var serviceProvider = DependencyInjectionHelper.BuildServiceProvider();
            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        /// <summary>
        /// A simple function that generates a pokemon
        /// </summary>
        /// <param name="createRandomPokemon"></param>
        /// <returns>A pokemon</returns>
        [HttpPost(Name = "CreateRandomPokemon")]
        public async Task<Pokemon> CreateRandomPokemonRestAsync(CreateRandomPokemon createRandomPokemon)
        {
            var pokemon = await _mediator.Send(createRandomPokemon);
            return pokemon;
        }
    }
}
