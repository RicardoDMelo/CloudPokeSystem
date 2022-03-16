using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Incubator.Domain.SpeciesAggregate;
using System;

namespace PokemonSystem.Incubator.Domain.PokemonAggregate
{
    public class IncubatorService : IIncubatorService
    {
        private readonly ISpeciesRepository _speciesRepository;
        private readonly Random _random;

        public IncubatorService(ISpeciesRepository speciesRepository)
        {
            _random = new Random();
            _speciesRepository = speciesRepository ?? throw new ArgumentNullException(nameof(speciesRepository));
        }

        public Pokemon GenerateRandomPokemon(string nickname = null, Level levelToGrow = null)
        {
            var species = _speciesRepository.GetRandomSpecies();
            var gender = _random.NextDouble() <= species.MaleFactor ? Gender.Male : Gender.Female;

            return new Pokemon(nickname, species, gender, levelToGrow);

        }
    }
}
