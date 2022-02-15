using NUnit.Framework;
using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Incubator.SpeciesAggregate;
using System;

namespace PokemonSystem.Tests.Incubator
{
    public class MoveByLevelTests
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
            Assert.Throws<ArgumentNullException>(() => new MoveByLevel(null, Moves.Tackle));
            Assert.Throws<ArgumentNullException>(() => new MoveByLevel(Levels.One, null));
        }

        [Test]
        public void Create_Species()
        {
            var moveByLevel = new MoveByLevel(Levels.Max, Moves.TailWhip);

            Assert.AreEqual(Levels.Max, moveByLevel.Level);
            Assert.AreEqual(Moves.TailWhip, moveByLevel.Move);
        }
    }
}