using PokemonSystem.Common.Enums;
using PokemonSystem.Incubator.Domain.PokemonAggregate;
using System.Text.Json;

namespace PokemonSystem.Incubator.Application.IntegrationEvent
{
    public class PokemonCreatedIntegrationEvent
    {
        public PokemonCreatedIntegrationEvent(Pokemon pokemon)
        {
            Id = pokemon.Id;
            Nickname = pokemon.Nickname;
            LevelToGrow = pokemon.LevelToGrow.Value;
            Gender = pokemon.Gender;
            SpeciesId = pokemon.PokemonSpecies.Id;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public Guid Id { get; private set; }
        public string Nickname { get; private set; }
        public uint LevelToGrow { get; private set; }
        public uint SpeciesId { get; private set; }
        public Gender Gender { get; private set; }
    }
}
