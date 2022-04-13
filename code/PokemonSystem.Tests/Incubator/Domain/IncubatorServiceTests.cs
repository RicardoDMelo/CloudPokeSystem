using NSubstitute;
using NUnit.Framework;
using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Incubator.Application.Adapters;
using PokemonSystem.Incubator.Domain;
using PokemonSystem.Incubator.Domain.SpeciesAggregate;
using PokemonSystem.Incubator.Infra.DatabaseDtos;
using PokemonSystem.Tests.Incubator.Builders;

namespace PokemonSystem.Tests.Incubator.Domain
{
    public class IncubatorServiceTests
    {
        private ISpeciesRepository? _speciesRepository;
        private ISpeciesAdapter? _speciesAdapter;

        private SpeciesBuilder? _speciesBuilder;
        private IncubatorService? _incubatorService;

        [SetUp]
        public void Setup()
        {
            _speciesRepository = Substitute.For<ISpeciesRepository>();
            _speciesAdapter = Substitute.For<ISpeciesAdapter>();
            _speciesBuilder = new SpeciesBuilder();
            _incubatorService = new IncubatorService(_speciesRepository, _speciesAdapter);
        }

        [Test]
        public async Task Create_Pokemon_With_No_Nickname()
        {
            var species = _speciesBuilder!.Build();
            var speciesDynamoDb = _speciesBuilder.ConvertToDynamoDb(species);

            _speciesRepository!.GetRandomSpeciesAsync().Returns(speciesDynamoDb);
            _speciesAdapter!.ConvertToModel(Arg.Any<SpeciesDynamoDb>()).Returns(species);
            var pokemon = await _incubatorService!.GenerateRandomPokemonAsync(null, null);

            Assert.IsEmpty(pokemon.Nickname);
            Assert.AreEqual(species, pokemon.PokemonSpecies);
        }

        [Test]
        public async Task Create_Pokemon_With_Nickname()
        {
            var nickname = "Toti";
            var species = _speciesBuilder!.Build();
            var speciesDynamoDb = _speciesBuilder.ConvertToDynamoDb(species);

            _speciesRepository!.GetRandomSpeciesAsync().Returns(speciesDynamoDb);
            _speciesAdapter!.ConvertToModel(Arg.Any<SpeciesDynamoDb>()).Returns(species);
            var pokemon = await _incubatorService!.GenerateRandomPokemonAsync(nickname, null);

            Assert.AreEqual(nickname, pokemon.Nickname);
            Assert.AreEqual(species, pokemon.PokemonSpecies);
        }

        [Test]
        public async Task Create_Pokemon_With_Level()
        {
            var level = new Level(50);
            var species = _speciesBuilder!.Build();
            var speciesDynamoDb = _speciesBuilder.ConvertToDynamoDb(species);

            _speciesRepository!.GetRandomSpeciesAsync().Returns(speciesDynamoDb);
            _speciesAdapter!.ConvertToModel(Arg.Any<SpeciesDynamoDb>()).Returns(species);
            var pokemon = await _incubatorService!.GenerateRandomPokemonAsync(null, level);

            Assert.AreEqual(level, pokemon.LevelToGrow);
            Assert.AreEqual(species, pokemon.PokemonSpecies);
        }

        [Test]
        public async Task Create_Male_Pokemon()
        {
            var species = _speciesBuilder!.WithMaleFactor(1).Build();
            var speciesDynamoDb = _speciesBuilder.ConvertToDynamoDb(species);

            _speciesRepository!.GetRandomSpeciesAsync().Returns(speciesDynamoDb);
            _speciesAdapter!.ConvertToModel(Arg.Any<SpeciesDynamoDb>()).Returns(species);
            var pokemon = await _incubatorService!.GenerateRandomPokemonAsync(null, null);

            Assert.AreEqual(species, pokemon.PokemonSpecies);
            Assert.AreEqual(Gender.Male, pokemon.Gender);
        }

        [Test]
        public async Task Create_Female_Pokemon()
        {
            var species = _speciesBuilder!.WithMaleFactor(0).Build();
            var speciesDynamoDb = _speciesBuilder.ConvertToDynamoDb(species);

            _speciesRepository!.GetRandomSpeciesAsync().Returns(speciesDynamoDb);
            _speciesAdapter!.ConvertToModel(Arg.Any<SpeciesDynamoDb>()).Returns(species);
            var pokemon = await _incubatorService!.GenerateRandomPokemonAsync(null, null);

            Assert.AreEqual(species, pokemon.PokemonSpecies);
            Assert.AreEqual(Gender.Female, pokemon.Gender);
        }
    }
}