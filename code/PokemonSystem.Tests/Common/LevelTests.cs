using NUnit.Framework;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Tests.ValueObjects;
using System;

namespace PokemonSystem.Tests.Common
{
    public class LevelTests
    {
        [Test]
        public void Create_Level_Minimum()
        {
            uint levelValue = 1;
            var level = new Level(levelValue);

            Assert.AreEqual(levelValue, level.Value);
        }

        [Test]
        public void Create_Level_Maximum()
        {
            uint levelValue = 100;
            var level = new Level(levelValue);

            Assert.AreEqual(levelValue, level.Value);
        }

        [Test]
        public void Error_Create_Level_Less_Than_1()
        {
            uint levelValue = 0;

            Assert.Throws<ArgumentException>(() => new Level(levelValue));
        }

        [Test]
        public void Error_Create_Level_Greater_Than_100()
        {
            uint levelValue = 101;

            Assert.Throws<ArgumentException>(() => new Level(levelValue));
        }

        [Test]
        public void Level_Should_Be_Equal()
        {
            uint levelValue = 1;
            var level1 = new Level(levelValue);
            var level2 = new Level(levelValue);

            Assert.AreEqual(level1, level2);
        }

        [Test]
        public void Level_Should_Not_Be_Equal()
        {
            uint levelValue = 1;
            var level1 = new Level(levelValue);
            var level2 = new Level(++levelValue);

            Assert.AreNotEqual(level1, level2);
        }

        [Test]
        public void Level_Compare_Should_Be_Equal()
        {
            uint levelValue = 1;
            var level1 = new Level(levelValue);
            var level2 = new Level(levelValue);

            Assert.AreEqual(0, level1.CompareTo(level2));
        }

        [Test]
        public void Level_Should_Be_Greater()
        {
            uint levelValue = 1;
            var level1 = new Level(levelValue);
            var level2 = new Level(++levelValue);

            Assert.Greater(level2, level1);
        }

        [Test]
        public void Level_Should_Be_Lesser()
        {
            uint levelValue = 5;
            var level1 = new Level(levelValue);
            var level2 = new Level(--levelValue);

            Assert.Less(level2, level1);
        }

        [Test]
        public void Exception_Compare_To_Null()
        {
            uint levelValue = 1;
            var level = new Level(levelValue);

            Assert.Throws<ArgumentNullException>(() => level.CompareTo(null));
        }

        [Test]
        public void Exception_Compare_To_Other_Type()
        {
            uint levelValue = 1;
            var level = new Level(levelValue);
            var obj = new object();

            Assert.Throws<ArgumentException>(() => level.CompareTo(obj));
        }

        [Test]
        public void Level_Equal_Operator()
        {
            uint levelValue = 1;
            var level1 = new Level(levelValue);
            var level2 = new Level(levelValue);

            Assert.True(level1 == level2);
        }

        [Test]
        public void Level_Different_Operator()
        {
            uint levelValue = 1;
            var level1 = new Level(levelValue);
            var level2 = new Level(++levelValue);

            Assert.True(level1 != level2);
        }

        [Test]
        public void Level_Greater_Operator()
        {
            uint levelValue = 2;
            var level1 = new Level(levelValue);
            var level2 = new Level(--levelValue);

            Assert.True(level1 > level2);
        }

        [Test]
        public void Level_Greater_Or_Equals_Operator()
        {
            uint levelValue = 2;
            var level1 = new Level(levelValue);
            var level2 = new Level(--levelValue);

            Assert.True(level1 >= level2);
        }

        [Test]
        public void Level_Less_Operator()
        {
            uint levelValue = 1;
            var level1 = new Level(levelValue);
            var level2 = new Level(++levelValue);

            Assert.True(level1 < level2);
        }

        [Test]
        public void Level_Less_Or_Equal_Operator()
        {
            uint levelValue = 1;
            var level1 = new Level(levelValue);
            var level2 = new Level(++levelValue);

            Assert.True(level1 <= level2);
        }

        [Test]
        public void Level_GetHashcode()
        {
            uint levelValue = 1;
            var level1 = new Level(levelValue);
            var level2 = new Level(levelValue);

            Assert.AreEqual(level1.GetHashCode(), level2.GetHashCode());
        }
    }
}