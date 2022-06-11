using PokemonSystem.Common.Enums;
using PokemonSystem.Common.SeedWork.Domain;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Incubator.Domain.SpeciesAggregate;

namespace PokemonSystem.Incubator.Domain.PokemonAggregate
{
    public class Pokemon : Entity, IAggregateRoot
    {
        public Pokemon(string nickname, Species pokemonSpecies, Level levelToGrow) : base()
        {
            Nickname = nickname;
            PokemonSpecies = pokemonSpecies;

            if (pokemonSpecies.MaleFactor is null)
            {
                Gender = Gender.Undefined;
            }
            else
            {
                Gender = Random.Shared.NextDouble() <= pokemonSpecies.MaleFactor ? Gender.Male : Gender.Female;
            }

            LevelToGrow = levelToGrow;

            AddDomainEvent(new PokemonCreatedDomainEvent(this));
        }

        public string Nickname { get; private set; }
        public Level LevelToGrow { get; private set; }
        public Species PokemonSpecies { get; private set; }
        public Gender Gender { get; private set; }
    }
}
