using MediatR;
using PokemonSystem.Learning.Domain.PokemonAggregate;

namespace PokemonSystem.Learning.Application.Commands
{
    public class TeachPokemonMoves : IRequest<Pokemon>
    {
        public TeachPokemonMoves(Guid id, uint level, uint speciesId)
        {
            Id = id;
            Level = level;
            SpeciesId = speciesId;
        }

        public Guid Id { get; set; }
        public uint Level { get; set; }
        public uint SpeciesId { get; set; }
    }
}
