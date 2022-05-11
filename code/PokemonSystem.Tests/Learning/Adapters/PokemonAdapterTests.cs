using NUnit.Framework;
using PokemonSystem.Learning.Infra.Adapters;
using PokemonSystem.Tests.Learning.Builders;

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
        public void Adapt()
        {
            var pokemon = _pokemonBuilder!.Build();
            var pokemonDynamoDb = _pokemonBuilder!.ConvertToDynamoDb(pokemon);

            var result = _adapter!.ConvertToModel(pokemonDynamoDb);

            Assert.AreEqual(pokemon, result);
        }

    }
}
