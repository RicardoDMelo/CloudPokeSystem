using NUnit.Framework;
using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;
using System;

namespace PokemonSystem.Tests.Common
{
    public class MoveTests
    {
        [Test]
        public void Create_Move()
        {
            string name = "Tackle";
            PokemonType type = PokemonType.Normal;
            MoveCategory category = MoveCategory.Physical;
            uint? power = 50;
            double accuracy = 0.5;
            uint pp = 30;

            var move = new Move(name, type, category, power, accuracy, pp);

            Assert.AreEqual(name, move.Name);
            Assert.AreEqual(type, move.Type);
            Assert.AreEqual(category, move.Category);
        }

        [Test]
        public void Create_Move_Status_Move()
        {
            string name = "Tackle";
            PokemonType type = PokemonType.Normal;
            MoveCategory category = MoveCategory.Status;
            uint? power = null;
            double accuracy = 0.5;
            uint pp = 30;

            var move = new Move(name, type, category, power, accuracy, pp);

            Assert.AreEqual(name, move.Name);
            Assert.AreEqual(type, move.Type);
            Assert.AreEqual(category, move.Category);
            Assert.AreEqual(power, move.Power);
        }

        [Test]
        public void Error_Create_Move_Wrong_Accuracy()
        {
            string name = "Tackle";
            PokemonType type = PokemonType.Normal;
            MoveCategory category = MoveCategory.Status;
            uint? power = null;
            double accuracy = 2;
            uint pp = 30;

            Assert.Throws<ArgumentException>(() => new Move(name, type, category, power, accuracy, pp));
        }

        [Test]
        public void Move_Should_Be_Equal()
        {
            string name = "Tackle";
            PokemonType type = PokemonType.Normal;
            MoveCategory category = MoveCategory.Status;
            uint? power = null;
            double accuracy = 0.5;
            uint pp = 30;

            var move1 = new Move(name, type, category, power, accuracy, pp);
            var move2 = new Move(name, type, category, power, accuracy, pp);

            Assert.AreEqual(move1, move2);
        }

        [Test]
        public void Move_Should_Not_Be_Equal()
        {
            string name = "Tackle";
            PokemonType type = PokemonType.Normal;
            MoveCategory category = MoveCategory.Status;
            uint? power = null;
            double accuracy = 0.5;
            uint pp = 30;

            var move1 = new Move(name, type, category, power, accuracy, pp);
            var move2 = new Move(name, type, category, power, accuracy, ++pp);

            Assert.AreNotEqual(move1, move2);
        }
    }
}