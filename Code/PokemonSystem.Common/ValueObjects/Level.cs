using PokemonSystem.Common.Properties;
using PokemonSystem.Common.SeedWork;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace PokemonSystem.Common.ValueObjects
{
    public class Level : ValueObject, IComparable
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

        public int CompareTo([AllowNull] object other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));
            if (other.GetType() != typeof(Level))
                throw new ArgumentException(string.Format(Errors.NotTheSameType, nameof(other)));

            var level = (Level)other;
            if (this.Value < level.Value)
                return -1;
            else if (this.Value > level.Value)
                return 1;

            return 0;
        }

        public static bool operator ==(Level x, Level y)
        {
            return x.CompareTo(y) == 0;
        }

        public static bool operator !=(Level x, Level y)
        {
            return x.CompareTo(y) != 0;
        }

        public static bool operator <(Level x, Level y)
        {
            return x.CompareTo(y) == -1;
        }

        public static bool operator >(Level x, Level y)
        {
            return x.CompareTo(y) == 1;
        }

        public static bool operator <=(Level x, Level y)
        {
            var result = x.CompareTo(y);
            return result == -1 || result == 0;
        }

        public static bool operator >=(Level x, Level y)
        {
            var result = x.CompareTo(y);
            return result == 1 || result == 0;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
