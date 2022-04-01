using NUnit.Framework;
using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;
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
        public void Create_Pokemon()
        {
            var species = _speciesBuilder.Build();
            var nickname = "Albero";
            var level = new Level(10);

            var pokemon = new Pokemon(nickname, species,  level);

            Assert.AreEqual(nickname, pokemon.Nickname);
            Assert.AreEqual(species, pokemon.PokemonSpecies);
            Assert.AreEqual(level, pokemon.LevelToGrow);
        }

        [Test]
        public void Create_Male_Pokemon()
        {
            var species = _speciesBuilder.WithMaleFactor(1).Build();
            var nickname = "Albero";
            var level = new Level(10);

            var pokemon = new Pokemon(nickname, species, level);

            Assert.AreEqual(species, pokemon.PokemonSpecies);
            Assert.AreEqual(Gender.Male, pokemon.Gender);
        }

        [Test]
        public void Create_Female_Pokemon()
        {
            var species = _speciesBuilder.WithMaleFactor(0).Build();
            var nickname = "Albero";
            var level = new Level(10);

            var pokemon = new Pokemon(nickname, species, level);

            Assert.AreEqual(species, pokemon.PokemonSpecies);
            Assert.AreEqual(Gender.Female, pokemon.Gender);
        }
    }
}