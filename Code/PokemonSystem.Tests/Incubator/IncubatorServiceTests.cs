using NSubstitute;
using NUnit.Framework;
using PokemonSystem.Common.Enums;
using PokemonSystem.Incubator.Domain.PokemonAggregate;
using PokemonSystem.Incubator.Domain.SpeciesAggregate;
using PokemonSystem.Tests.Incubator.Builders;
using System;

namespace PokemonSystem.Tests.Incubator
{
    public class IncubatorServiceTests
    {
        private ISpeciesRepository _speciesRepository;

        private SpeciesBuilder _speciesBuilder;
        private IncubatorService _incubatorService;

        [SetUp]
        public void Setup()
        {
            _speciesRepository = Substitute.For<ISpeciesRepository>();
            _speciesBuilder = new SpeciesBuilder();
            _incubatorService = new IncubatorService(_speciesRepository);
        }

        [Test]
        public void Constructor_Null_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => new IncubatorService(null));
        }

        [Test]
        public void Create_Pokemon_With_No_Nickname()
        {
            var species = _speciesBuilder.Build();

            _speciesRepository.GetRandomSpecies().Returns(species);
            var pokemon = _incubatorService.GenerateRandomPokemon();

            Assert.IsNull(pokemon.Nickname);
            Assert.AreEqual(species, pokemon.PokemonSpecies);
        }

        [Test]
        public void Create_Pokemon_With_Nickname()
        {
            var nickname = "Toti";
            var species = _speciesBuilder.Build();

            _speciesRepository.GetRandomSpecies().Returns(species);
            var pokemon = _incubatorService.GenerateRandomPokemon(nickname);

            Assert.AreEqual(nickname, pokemon.Nickname);
            Assert.AreEqual(species, pokemon.PokemonSpecies);
        }

        [Test]
        public void Create_Male_Pokemon()
        {
            var species = _speciesBuilder.WithMaleFactor(1).Build();

            _speciesRepository.GetRandomSpecies().Returns(species);
            var pokemon = _incubatorService.GenerateRandomPokemon();

            Assert.AreEqual(species, pokemon.PokemonSpecies);
            Assert.AreEqual(Gender.Male, pokemon.Gender);
        }

        [Test]
        public void Create_Female_Pokemon()
        {
            var species = _speciesBuilder.WithMaleFactor(0).Build();

            _speciesRepository.GetRandomSpecies().Returns(species);
            var pokemon = _incubatorService.GenerateRandomPokemon();

            Assert.AreEqual(species, pokemon.PokemonSpecies);
            Assert.AreEqual(Gender.Female, pokemon.Gender);
        }
    }
}