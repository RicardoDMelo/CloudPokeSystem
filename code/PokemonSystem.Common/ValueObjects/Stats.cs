using PokemonSystem.Common.SeedWork.Domain;
using System.Collections.Generic;

namespace PokemonSystem.Common.ValueObjects
{
    public class Stats : ValueObject
    {
        public Stats(uint hp, uint attack, uint defense, uint specialAttack, uint specialDefense, uint speed)
        {
            HP = hp;
            Attack = attack;
            Defense = defense;
            SpecialAttack = specialAttack;
            SpecialDefense = specialDefense;
            Speed = speed;
        }

        public uint HP { get; private set; }
        public uint Attack { get; private set; }
        public uint Defense { get; private set; }
        public uint SpecialAttack { get; private set; }
        public uint SpecialDefense { get; private set; }
        public uint Speed { get; private set; }

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
