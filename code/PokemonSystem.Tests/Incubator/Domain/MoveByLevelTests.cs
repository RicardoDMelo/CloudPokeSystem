using NUnit.Framework;
using PokemonSystem.Incubator.Domain.SpeciesAggregate;
using PokemonSystem.Tests.ValueObjects;
using System;

namespace PokemonSystem.Tests.Incubator.Domain
{
    public class MoveByLevelTests
    {
        [Test]
        public void Create_Species()
        {
            var moveByLevel = new MoveByLevel(Levels.Max, Moves.TailWhip);

            Assert.AreEqual(Levels.Max, moveByLevel.Level);
            Assert.AreEqual(Moves.TailWhip, moveByLevel.Move);
        }
    }
}