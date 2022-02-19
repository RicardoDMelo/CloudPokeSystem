using NUnit.Framework;
using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;

namespace PokemonSystem.Tests.Common
{
    public class TypingTests
    {
        [Test]
        public void Create_Typing_Single_Type()
        {
            PokemonType type1 = PokemonType.Dragon;

            var typing = new Typing(type1);

            Assert.AreEqual(type1, typing.Type1);
            Assert.IsNull(typing.Type2);
        }

        [Test]
        public void Create_Typing_Double_Type()
        {
            PokemonType type1 = PokemonType.Dragon;
            PokemonType type2 = PokemonType.Poison;

            var typing = new Typing(type1, type2);

            Assert.AreEqual(type1, typing.Type1);
            Assert.AreEqual(type2, typing.Type2);
        }

        [Test]
        public void Typing_Should_Be_Equal()
        {
            PokemonType type1 = PokemonType.Dragon;
            PokemonType type2 = PokemonType.Poison;

            var typing1 = new Typing(type1, type2);
            var typing2 = new Typing(type1, type2);

            Assert.AreEqual(typing1, typing2);
        }

        [Test]
        public void Typing_Should_Not_Be_Equal()
        {
            PokemonType type1 = PokemonType.Dragon;
            PokemonType type2 = PokemonType.Poison;
            PokemonType type3 = PokemonType.Flying;

            var typing1 = new Typing(type1, type2);
            var typing2 = new Typing(type1, type3);

            Assert.AreNotEqual(typing1, typing2);
        }
    }
}