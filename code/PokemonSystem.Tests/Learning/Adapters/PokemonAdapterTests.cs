using NUnit.Framework;
using PokemonSystem.Learning.Infra.Adapters;
using PokemonSystem.Tests.Learning.Builders;
using PokemonSystem.Tests.ValueObjects;

namespace PokemonSystem.Tests.Learning.Adapters
{
    internal class PokemonAdapterTests
    {
        private PokemonBuilder? _pokemonBuilder;

        private PokemonAdapter? _adapter;

        [SetUp]
        public void Setup()
        {
            _pokemonBuilder = new PokemonBuilder();
            _adapter = new PokemonAdapter();
        }

        [Test]
        public void Adapt_To_Model()
        {
            var pokemon = _pokemonBuilder!.WithLevel(Levels.Max).Build();
            var pokemonDynamoDb = _pokemonBuilder!.ConvertToDynamoDb(pokemon);

            var result = _adapter!.ConvertToModel(pokemonDynamoDb);

            Assert.AreEqual(pokemon, result);
            Assert.AreEqual(pokemon.LearntMoves.Values.ElementAt(0), result.LearntMoves.Values.ElementAt(0));
            Assert.AreEqual(pokemon.LearntMoves.Values.ElementAt(1), result.LearntMoves.Values.ElementAt(1));
            Assert.AreEqual(pokemon.LearntMoves.Values.ElementAt(2), result.LearntMoves.Values.ElementAt(2));
            Assert.AreEqual(pokemon.LearntMoves.Values.ElementAt(3), result.LearntMoves.Values.ElementAt(3));
        }

        [Test]
        public void Adapt_To_Dto()
        {
            var pokemon = _pokemonBuilder!.WithLevel(Levels.Max).Build();
            var pokemonDynamoDb = _pokemonBuilder!.ConvertToDynamoDb(pokemon);

            var result = _adapter!.ConvertToDto(pokemon);

            Assert.AreEqual(pokemonDynamoDb.Id, result.Id);
        }

    }
}
