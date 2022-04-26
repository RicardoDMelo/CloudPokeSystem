using MediatR;
using PokemonSystem.Learning.Domain.PokemonAggregate;
using System.Text.Json;

namespace PokemonSystem.Evolution.Application.Commands
{
    public class TeachPokemonMoves : IRequest<Pokemon>
    {
        public TeachPokemonMoves(Guid id, uint level, uint speciesId)
        {
            Id = id;
            Level = level;
            SpeciesId = speciesId;
        }

        public static TeachPokemonMoves FromString(string json)
        {
            return JsonSerializer.Deserialize<TeachPokemonMoves>(json)!;
        }

        public Guid Id { get; set; }
        public uint Level { get; set; }
        public uint SpeciesId { get; set; }
    }
}
