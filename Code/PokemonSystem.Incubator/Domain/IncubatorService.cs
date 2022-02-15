using PokemonSystem.Common.Enums;
using PokemonSystem.Incubator.Domain.SpeciesAggregate;
using System;

namespace PokemonSystem.Incubator.PokemonAggregate
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

        public Pokemon GenerateRandomPokemon(string nickname = null)
        {
            var species = _speciesRepository.GetRandomSpecies();
            var gender = _random.NextDouble();

            if (gender <= species.MaleFactor)
            {
                return new Pokemon(nickname, species, Gender.Male);
            }
            else
            {
                return new Pokemon(nickname, species, Gender.Female);
            }
        }
    }
}
