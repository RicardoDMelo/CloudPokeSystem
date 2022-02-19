using NUnit.Framework;
using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Incubator.Domain.SpeciesAggregate;
using PokemonSystem.Tests.Incubator.Builders;
using System;

namespace PokemonSystem.Tests.Incubator
{
    public class SpeciesTests
    {
        private MoveSetBuilder _moveSetBuilder;

        [SetUp]
        public void Setup()
        {
            _moveSetBuilder = new MoveSetBuilder();
        }

        [Test]
        public void Constructor_Null_Exception()
        {
            var number = 128;
            var name = "Tauros";
            var type = new Typing(PokemonType.Normal);
            var baseStats = new Stats(1, 2, 3, 4, 5, 6);
            EvolutionCriteria evolutionCriteria = null;
            var maleFactor = 0.6;
            var moveset = _moveSetBuilder.Build();

            Assert.Throws<ArgumentException>(() => new Species(number, null, type, baseStats, maleFactor, evolutionCriteria, moveset));
            Assert.Throws<ArgumentNullException>(() => new Species(number, name, type, null, maleFactor, evolutionCriteria, moveset));
            Assert.Throws<ArgumentNullException>(() => new Species(number, name, type, baseStats, maleFactor, evolutionCriteria, null));
        }

        [Test]
        public void Create_Species()
        {
            var number = 128;
            var name = "Tauros";
            var type = new Typing(PokemonType.Normal);
            var baseStats = new Stats(1, 2, 3, 4, 5, 6);
            EvolutionCriteria evolutionCriteria = null;
            var maleFactor = 0.6;
            var moveset = _moveSetBuilder.Build();

            var species = new Species(
                number,
                name,
                type,
                baseStats,
                maleFactor,
                evolutionCriteria,
                moveset
            );

            Assert.AreEqual(number, species.Number);
            Assert.AreEqual(name, species.Name);
            Assert.AreEqual(type, species.Typing);
            Assert.AreEqual(moveset.Count, species.MoveSet.Count);
            Assert.AreEqual(moveset, species.MoveSet);
        }
    }
}