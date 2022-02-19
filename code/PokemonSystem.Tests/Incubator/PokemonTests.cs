using NUnit.Framework;
using PokemonSystem.Common.Enums;
using PokemonSystem.Incubator.Domain.PokemonAggregate;
using PokemonSystem.Tests.Incubator.Builders;
using System;

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
        public void Constructor_Null_Exception()
        {
            var nickname = "Albero";
            var pokemonGender = Gender.Male;

            Assert.Throws<ArgumentNullException>(() => new Pokemon(nickname, null, pokemonGender));
        }

        [Test]
        public void Create_Pokemon()
        {
            var species = _speciesBuilder.Build();
            var nickname = "Albero";
            var pokemonGender = Gender.Male;

            var pokemon = new Pokemon(nickname, species, pokemonGender);

            Assert.AreEqual(nickname, pokemon.Nickname);
            Assert.AreEqual(species, pokemon.PokemonSpecies);
            Assert.AreEqual(pokemonGender, pokemon.Gender);
        }
    }
}