using MediatR;
using PokemonSystem.Evolution.Domain.PokemonAggregate;
using System.Text.Json;

namespace PokemonSystem.Evolution.Application.Commands
{
    public class GrantPokemonLevel : IRequest<Pokemon>
    {
        public GrantPokemonLevel(Guid id, uint levelToGrow, uint speciesId)
        {
            Id = id;
            LevelToGrow = levelToGrow;
            SpeciesId = speciesId;
        }

        public static GrantPokemonLevel FromString(string json)
        {
            return JsonSerializer.Deserialize<GrantPokemonLevel>(json)!;
        }

        public Guid Id { get; set; }
        public uint LevelToGrow { get; set; }
        public uint SpeciesId { get; set; }
    }
}
