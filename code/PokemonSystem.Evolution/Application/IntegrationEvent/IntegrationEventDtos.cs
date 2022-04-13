using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Evolution.Domain.PokemonAggregate;

namespace PokemonSystem.Evolution.Application.IntegrationEvent
{
    public class PokemonDto
    {
        public PokemonDto(Pokemon pokemon)
        {
            Id = pokemon.Id;
            Level = pokemon.Level.Value;
            SpeciesId = pokemon.PokemonSpecies.Id;
        }

        public Guid Id { get; private set; }
        public uint Level { get; private set; }
        public uint SpeciesId { get; private set; }
    }
}
