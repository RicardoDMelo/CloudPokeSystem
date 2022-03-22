using PokemonSystem.Common.SeedWork;
using PokemonSystem.Common.Enums;
using System;
using System.Collections.Generic;
using PokemonSystem.Common.Properties;

namespace PokemonSystem.Common.ValueObjects
{
    public class Move : ValueObject
    {
        const uint MIN_ACCURACY = 0;
        const uint MAX_ACCURACY = 1;

        public Move(string name, PokemonType type, MoveCategory category, uint? power, double accuracy, uint pp)
        {
            if (accuracy > MAX_ACCURACY || accuracy < MIN_ACCURACY)
            {
                throw new ArgumentException(string.Format(Errors.Between, nameof(accuracy), MIN_ACCURACY, MAX_ACCURACY));
            }

            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type;
            Category = category;
            Power = power;
            Accuracy = accuracy;
            PP = pp;
        }

        public string Name { get; private set; }
        public PokemonType Type { get; private set; }
        public MoveCategory Category { get; private set; }
        public uint? Power { get; private set; }
        public double Accuracy { get; private set; }
        public uint PP { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Type;
            yield return Category;
            yield return Power;
            yield return Accuracy;
            yield return PP;
        }

        public override string ToString()
        {
            return $"[{Type}] {Name}";
        }
    }
}
