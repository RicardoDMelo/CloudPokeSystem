using NUnit.Framework;
using PokemonSystem.Common.Enums;
using PokemonSystem.Incubator.PokemonAggregate;
using PokemonSystem.Incubator.SpeciesAggregate;

namespace PokemonSystem.Tests.Incubator
{
    public class PokemonTests
    {
        private SpeciesBuilder _speciesBuilder;

        [SetUp]
        public void Setup()
        {
            _speciesBuilder = new SpeciesBuilder();
        }

        [Test]
        public void Create_Pokemon()
        {
            var pokemonAlias = "Albero";
            var species = _speciesBuilder.Build();
            var pokemonGender = Gender.Male;

            var pokemon = new Pokemon(pokemonAlias, species, pokemonGender);

            Assert.AreEqual(pokemonAlias, pokemon.Alias);
            Assert.AreEqual(species, pokemon.PokemonSpecies);
            Assert.AreEqual(pokemonGender, pokemon.Gender);
        }
    }
}