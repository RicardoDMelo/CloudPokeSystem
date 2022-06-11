using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Learning.Domain;
using PokemonSystem.Learning.Domain.PokemonAggregate;
using PokemonSystem.Learning.Domain.SpeciesAggregate;
using PokemonSystem.Tests.Learning.Builders;
using PokemonSystem.Tests.ValueObjects;

namespace PokemonSystem.Tests.Learning.Domain
{
    public class LearningServiceTests
    {
        private IPokemonRepository? _pokemonRepository;
        private ISpeciesRepository? _speciesRepository;

        private SpeciesBuilder? _speciesBuilder;
        private PokemonBuilder? _pokemonBuilder;

        private LearningService? _learningService;

        [SetUp]
        public void Setup()
        {
            _pokemonRepository = Substitute.For<IPokemonRepository>();
            _speciesRepository = Substitute.For<ISpeciesRepository>();

            _speciesBuilder = new SpeciesBuilder();
            _pokemonBuilder = new PokemonBuilder();

            _learningService = new LearningService(_pokemonRepository, _speciesRepository);
        }

        [Test]
        public async Task Teach_Pokemon_Moves_For_Existent_Pokemon()
        {
            var species = _speciesBuilder!.Build();
            var pokemon = _pokemonBuilder!.WithLevel(Levels.One).Build();

            _pokemonRepository!.GetAsync(Arg.Is(pokemon.Id)).Returns(pokemon);
            var result = await _learningService!.TeachRandomMovesAsync(pokemon.Id, Levels.Ten, species.Id);

            await _pokemonRepository!.ReceivedWithAnyArgs(1).AddOrUpdateAsync(Arg.Any<Pokemon>());
            Assert.IsNotEmpty(result.LearntMoves.Values);
            Assert.AreEqual(pokemon.Id, result.Id);
            Assert.AreEqual(Levels.Ten, result.Level);
        }

        [Test]
        public async Task Teach_Pokemon_Moves_For_Inexistent_Pokemon()
        {
            var pokemonId = Guid.NewGuid();
            var species = _speciesBuilder!.Build();

            _pokemonRepository!.GetAsync(Arg.Is(pokemonId)).ReturnsNull();
            _speciesRepository!.GetAsync(Arg.Is(species.Id)).Returns(species);
            var result = await _learningService!.TeachRandomMovesAsync(pokemonId, Levels.Ten, species.Id);

            await _pokemonRepository!.ReceivedWithAnyArgs(1).AddOrUpdateAsync(Arg.Any<Pokemon>());
            Assert.IsNotEmpty(result.LearntMoves.Values);
            Assert.AreEqual(pokemonId, result.Id);
            Assert.AreEqual(Levels.Ten, result.Level);
        }
    }
}