using PokemonSystem.Common.SeedWork;
using System.Collections.Generic;

namespace PokemonSystem.Common.ValueObjects
{
    public class Stats : ValueObject
    {
        public Stats(int hp, int attack, int defense, int specialAttack, int specialDefense, int speed)
        {
            HP = hp;
            Attack = attack;
            Defense = defense;
            SpecialAttack = specialAttack;
            SpecialDefense = specialDefense;
            Speed = speed;
        }

        public int HP { get; private set; }
        public int Attack { get; private set; }
        public int Defense { get; private set; }
        public int SpecialAttack { get; private set; }
        public int SpecialDefense { get; private set; }
        public int Speed { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return HP;
            yield return Attack;
            yield return Defense;
            yield return SpecialAttack;
            yield return SpecialDefense;
            yield return Speed;
        }
    }
}
