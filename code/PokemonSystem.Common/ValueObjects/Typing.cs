﻿using PokemonSystem.Common.SeedWork;
using PokemonSystem.Common.Enums;
using System.Collections.Generic;

namespace PokemonSystem.Common.ValueObjects
{
    public class Typing : ValueObject
    {
        public Typing(PokemonType type1)
        {
            Type1 = type1;
            Type2 = null;
        }

        public Typing(PokemonType type1, PokemonType? type2)
        {
            Type1 = type1;
            Type2 = type2;
        }

        public PokemonType Type1 { get; private set; }
        public PokemonType? Type2 { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Type1;
            yield return Type2;
        }

        public override string ToString()
        {
            string text = Type1.ToString();
            if (Type2 != null)
            {
                text += " | " + Type2;
            }
            return text;
        }
    }
}
