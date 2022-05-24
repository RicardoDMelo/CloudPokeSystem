using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Evolution.Domain.PokemonAggregate;
using System.Text.Json;

namespace PokemonSystem.Evolution.Application.IntegrationEvent
{
    public class PokemonLevelRaisedIntegrationEvent
    {
        public PokemonLevelRaisedIntegrationEvent(Pokemon pokemon)
        {
            Id = pokemon.Id;
            Level = pokemon.Level.Value;
            SpeciesId = pokemon.PokemonSpecies.Id;
            Stats = pokemon.Stats;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public Guid Id { get; private set; }
        public uint Level { get; private set; }
        public uint SpeciesId { get; private set; }
        public Stats Stats { get; private set; }
    }
}
