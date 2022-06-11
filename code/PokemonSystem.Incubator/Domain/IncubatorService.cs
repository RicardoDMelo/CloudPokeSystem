using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Incubator.Domain.PokemonAggregate;
using PokemonSystem.Incubator.Domain.SpeciesAggregate;

namespace PokemonSystem.Incubator.Domain
{
    public class IncubatorService : IIncubatorService
    {
        private readonly ISpeciesRepository _speciesRepository;

        public IncubatorService(ISpeciesRepository speciesRepository)
        {
            _speciesRepository = speciesRepository;
        }

        public async Task<Pokemon> GenerateRandomPokemonAsync(string? nickname, Level? levelToGrow)
        {
            var species = await _speciesRepository.GetRandomSpeciesAsync();
            return new Pokemon(nickname ?? string.Empty, species, levelToGrow ?? new Level(1));
        }
    }
}
