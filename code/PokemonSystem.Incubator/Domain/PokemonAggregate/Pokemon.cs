using PokemonSystem.Common.Enums;
using PokemonSystem.Common.SeedWork;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Incubator.Domain.SpeciesAggregate;
using System;

namespace PokemonSystem.Incubator.Domain.PokemonAggregate
{
    public class Pokemon : Entity, IAggregateRoot
    {
        public Pokemon(string nickname, Species pokemonSpecies, Gender gender, Level levelToGrow = null) : base()
        {
            Nickname = nickname;
            PokemonSpecies = pokemonSpecies ?? throw new ArgumentNullException(nameof(pokemonSpecies));
            Gender = gender;
            LevelToGrow = levelToGrow;
        }

        public string Nickname { get; private set; }
        public Level LevelToGrow { get; private set; }
        public Species PokemonSpecies { get; private set; }
        public Gender Gender { get; private set; }
    }
}
