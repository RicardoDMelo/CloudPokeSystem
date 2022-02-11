using PokemonSystem.Common.Properties;
using PokemonSystem.Common.SeedWork;
using System;
using System.Collections.Generic;

namespace PokemonSystem.Common.ValueObjects
{
    public class Level : ValueObject
    {
        const uint MAX_LEVEL = 100;
        public Level(uint value)
        {
            if (value > 100)
            {
                throw new ArgumentException(string.Format(Errors.LessOrEquals, nameof(value), MAX_LEVEL));
            }

            Value = value;
        }

        public uint Value { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
