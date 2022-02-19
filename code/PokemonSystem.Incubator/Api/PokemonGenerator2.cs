
using Amazon.Lambda.Core;
using PokemonSystem.Incubator.Domain.PokemonAggregate;
using PokemonSystem.Incubator.Infra;

namespace PokemonSystem.Incubator.Api
{
    public class PokemonGenerator2
    {
        private readonly IIncubatorService _incubatorService;
        public PokemonGenerator2()
        {
            var repository = new SpeciesRepository();
            _incubatorService = new IncubatorService(repository);
        }

        /// <summary>
        /// A simple function that generates a pokemon
        /// </summary>
        /// <param name="context"></param>
        /// <returns>A pokemon</returns>
        public Pokemon FunctionHandler(ILambdaContext context)
        {
            var pokemon = _incubatorService.GenerateRandomPokemon();
            return pokemon;
        }
    }
}
