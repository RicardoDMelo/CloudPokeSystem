using PokemonSystem.Evolution.Domain.PokemonAggregate;
using System.Text.Json;

namespace PokemonSystem.Evolution.Application.IntegrationEvent
{
    public class PokemonEvolvedIntegrationEvent
    {
        public PokemonEvolvedIntegrationEvent(Pokemon pokemon)
        {
            Pokemon = new PokemonDto(pokemon);
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public PokemonDto Pokemon { get; set; }
    }
}
