using NUnit.Framework;
using PokemonSystem.Common.ValueObjects;

namespace PokemonSystem.Tests.Common
{
    public class StatsTests
    {
        [Test]
        public void Create_Stats()
        {
            uint hp = 1;
            uint attack = 2;
            uint defense = 3;
            uint specialAttack = 4;
            uint specialDefense = 5;
            uint speed = 6;

            var stats = new Stats(hp, attack, defense, specialAttack, specialDefense, speed);

            Assert.AreEqual(hp, stats.HP);
            Assert.AreEqual(attack, stats.Attack);
            Assert.AreEqual(speed, stats.Speed);
        }

        [Test]
        public void Stats_Should_Be_Equal()
        {
            uint hp = 1;
            uint attack = 2;
            uint defense = 3;
            uint specialAttack = 4;
            uint specialDefense = 5;
            uint speed = 6;

            var stats1 = new Stats(hp, attack, defense, specialAttack, specialDefense, speed);
            var stats2 = new Stats(hp, attack, defense, specialAttack, specialDefense, speed);

            Assert.AreEqual(stats1, stats2);
        }

        [Test]
        public void Stats_Should_Not_Be_Equal()
        {
            uint hp = 1;
            uint attack = 2;
            uint defense = 3;
            uint specialAttack = 4;
            uint specialDefense = 5;
            uint speed = 6;

            var stats1 = new Stats(hp, attack, defense, specialAttack, specialDefense, speed);
            var stats2 = new Stats(hp, attack, defense, specialAttack, specialDefense, ++speed);

            Assert.AreNotEqual(stats1, stats2);
        }
    }
}