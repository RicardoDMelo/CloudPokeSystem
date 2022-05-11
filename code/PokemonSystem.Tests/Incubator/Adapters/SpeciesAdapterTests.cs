using NUnit.Framework;
using PokemonSystem.Incubator.Infra.Adapters;
using PokemonSystem.Tests.Incubator.Builders;

namespace PokemonSystem.Tests.Incubator.Adapters
{
    internal class SpeciesAdapterTests
    {
        private SpeciesBuilder? _speciesBuilder;

        private SpeciesAdapter? _adapter;

        [SetUp]
        public void Setup()
        {
            _speciesBuilder = new SpeciesBuilder();
            _adapter = new SpeciesAdapter();
        }

        [Test]
        public void Adapt()
        {
            var species = _speciesBuilder!.Build();
            var speciesDynamoDb = _speciesBuilder!.ConvertToDynamoDb(species);

            var result = _adapter!.ConvertToModel(speciesDynamoDb);

            Assert.AreEqual(species, result);
        }

    }
}
