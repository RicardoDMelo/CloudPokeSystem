using PokemonSystem.Common.Enums;
using PokemonSystem.Common.SeedWork;
using PokemonSystem.Incubator.SpeciesAggregate;
using System;

namespace PokemonSystem.Incubator.PokemonAggregate
{
    public class Pokemon : Entity, IAggregateRoot
    {
        public Pokemon(string alias, Species pokemonSpecies, Gender gender)
        {
            Alias = alias ?? throw new ArgumentNullException(nameof(alias));
            PokemonSpecies = pokemonSpecies ?? throw new ArgumentNullException(nameof(pokemonSpecies));
            Gender = gender;
        }

        public string Alias { get; private set; }
        public Species PokemonSpecies { get; private set; }
        public Gender Gender { get; private set; }
    }
}
