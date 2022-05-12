using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Learning.Domain.PokemonAggregate;
using PokemonSystem.Learning.Domain.SpeciesAggregate;

namespace PokemonSystem.Learning.Domain
{
    public class LearningService : ILearningService
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly ISpeciesRepository _speciesRepository;

        public LearningService(IPokemonRepository pokemonRepository, ISpeciesRepository speciesRepository)
        {
            _pokemonRepository = pokemonRepository;
            _speciesRepository = speciesRepository;
        }

        public async Task<Pokemon> TeachRandomMovesAsync(Guid pokemonId, Level level, uint speciesId)
        {
            var pokemon = await _pokemonRepository.GetAsync(pokemonId);

            if (pokemon is null)
            {
                var species = await _speciesRepository.GetAsync(speciesId);
                pokemon = new Pokemon(pokemonId, species, level);
                await _pokemonRepository.AddOrUpdateAsync(pokemon);
            }
            else
            {
                pokemon.GrowToLevel(level);
                await _pokemonRepository.AddOrUpdateAsync(pokemon);
            }

            return pokemon;
        }
    }
}
