
using Amazon.Lambda.Core;
using PokemonSystem.Incubator.Domain.PokemonAggregate;
using PokemonSystem.Incubator.Infra;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace PokemonSystem.Incubator.Api
{
    public class IncubatorFunction
    {
        private readonly IIncubatorService _incubatorService;
        public IncubatorFunction()
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
