using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Learning.Domain.PokemonAggregate;
using System.Text.Json;

namespace PokemonSystem.Incubator.Application.IntegrationEvent
{
    public class PokemonLearnedMovesIntegrationEvent
    {
        public PokemonLearnedMovesIntegrationEvent(Pokemon pokemon)
        {
            Id = pokemon.Id;
            Level = pokemon.Level.Value;
            LearntMoves = pokemon.LearntMoves.Values.Select(x => new MoveDto(x)).ToList();
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public Guid Id { get; private set; }
        public uint Level { get; private set; }
        public List<MoveDto> LearntMoves { get; private set; }
    }

    public class MoveDto
    {
        public MoveDto(Move move)
        {
            Name = move.Name;
            Type = move.Type;
            Category = move.Category;
            Power = move.Power;
            Accuracy = move.Accuracy;
            PP = move.PP;
        }

        public string Name { get; private set; }
        public PokemonType Type { get; private set; }
        public MoveCategory Category { get; private set; }
        public uint? Power { get; private set; }
        public double Accuracy { get; private set; }
        public uint PP { get; private set; }
    }
}
