using PokemonSystem.Evolution.Domain.PokemonAggregate;
using System.Text.Json;

namespace PokemonSystem.Evolution.Application.IntegrationEvent
{
    public class PokemonEvolvedIntegrationEvent
    {
        public PokemonEvolvedIntegrationEvent(Pokemon pokemon)
        {
            Id = pokemon.Id;
            Level = pokemon.Level.Value;
            SpeciesId = pokemon.PokemonSpecies.Id;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public Guid Id { get; private set; }
        public uint Level { get; private set; }
        public uint SpeciesId { get; private set; }
    }
}
