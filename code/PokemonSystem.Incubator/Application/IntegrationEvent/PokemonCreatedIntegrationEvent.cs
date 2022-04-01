using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Incubator.Domain.PokemonAggregate;
using PokemonSystem.Incubator.Domain.SpeciesAggregate;
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
            PokemonSpecies = new(pokemon.PokemonSpecies);
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public Guid Id { get; private set; }
        public string Nickname { get; private set; }
        public uint LevelToGrow { get; private set; }
        public SpeciesDto PokemonSpecies { get; private set; }
        public Gender Gender { get; private set; }
    }

    #region Integration Dtos
    public class SpeciesDto
    {
        public SpeciesDto(Species species)
        {
            Name = species.Name;
            Typing = species.Typing;
            BaseStats = species.BaseStats;
            MaleFactor = species.MaleFactor;
            EvolutionCriterias = species.EvolutionCriterias.Select(x => new EvolutionCriteriaDto(x));
            MoveSet = species.MoveSet.Select(x => new MoveByLevelDto(x));
        }

        public string Name { get; private set; }
        public Typing Typing { get; private set; }
        public Stats BaseStats { get; private set; }
        public double? MaleFactor { get; private set; }
        public IEnumerable<EvolutionCriteriaDto> EvolutionCriterias { get; private set; }
        public IEnumerable<MoveByLevelDto> MoveSet { get; private set; }
    }

    public class EvolutionCriteriaDto
    {
        public EvolutionCriteriaDto(EvolutionCriteria evolutionCriteria)
        {
            EvolutionType = evolutionCriteria.EvolutionType;
            MinimumLevel = evolutionCriteria.MinimumLevel;
            Item = evolutionCriteria.Item;
            Species = new SpeciesDto(evolutionCriteria.Species);
        }

        public EvolutionType EvolutionType { get; private set; }
        public Level? MinimumLevel { get; private set; }
        public string? Item { get; private set; }
        public SpeciesDto Species { get; private set; }
    }

    public class MoveByLevelDto
    {
        public MoveByLevelDto(MoveByLevel moveByLevel)
        {
            Level = moveByLevel.Level;
            Move = moveByLevel.Move;
        }

        public Level Level { get; private set; }
        public Move Move { get; private set; }
    }
    #endregion
}
