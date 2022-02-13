using PokemonSystem.Common.SeedWork;
using PokemonSystem.Common.Enums;
using System;
using System.Collections.Generic;

namespace PokemonSystem.Common.ValueObjects
{
    public class Move : ValueObject
    {
        public Move(string name, PokemonType type, MoveCategory category, int? power, double accuracy, int pp)
        {
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
        public int? Power { get; private set; }
        public double Accuracy { get; private set; }
        public int PP { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Type;
            yield return Category;
            yield return Power;
            yield return Accuracy;
            yield return PP;
        }
    }
}
