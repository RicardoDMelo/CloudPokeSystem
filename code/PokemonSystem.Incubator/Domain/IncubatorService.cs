using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Incubator.Application.Adapters;
using PokemonSystem.Incubator.Domain.PokemonAggregate;
using PokemonSystem.Incubator.Domain.SpeciesAggregate;

namespace PokemonSystem.Incubator.Domain
{
    public class IncubatorService : IIncubatorService
    {
        private readonly ISpeciesRepository _speciesRepository;
        private readonly ISpeciesAdapter _speciesAdapter;

        public IncubatorService(ISpeciesRepository speciesRepository, ISpeciesAdapter speciesAdapter)
        {
            _speciesRepository = speciesRepository;
            _speciesAdapter = speciesAdapter;
        }

        public async Task<Pokemon> GenerateRandomPokemonAsync(string? nickname, Level? levelToGrow)
        {
            var speciesDto = await _speciesRepository.GetRandomSpeciesAsync();
            var species = _speciesAdapter.ConvertToModel(speciesDto);
            return new Pokemon(nickname ?? string.Empty, species, levelToGrow ?? new Level(1));
        }
    }
}
