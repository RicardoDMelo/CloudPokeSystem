using NUnit.Framework;
using PokemonSystem.Incubator.Domain.SpeciesAggregate;
using PokemonSystem.Tests.ValueObjects;
using System;

namespace PokemonSystem.Tests.Incubator
{
    public class MoveByLevelTests
    {
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