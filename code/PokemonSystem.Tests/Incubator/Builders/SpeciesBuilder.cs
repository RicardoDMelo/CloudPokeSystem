using AutoMapper;
using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Incubator.Domain.SpeciesAggregate;
using PokemonSystem.Incubator.Infra.DatabaseDtos;
using MoveByLevelDynamoDb = PokemonSystem.Incubator.Infra.DatabaseDtos.MoveByLevelDynamoDb;
using MoveDynamoDb = PokemonSystem.Incubator.Infra.DatabaseDtos.MoveDynamoDb;
using SpeciesDynamoDb = PokemonSystem.Incubator.Infra.DatabaseDtos.SpeciesDynamoDb;

namespace PokemonSystem.Tests.Incubator.Builders
{
    public class SpeciesBuilder
    {
        private uint _number = 128;
        private string _name = "Tauros";
        private Typing _type = new Typing(PokemonType.Normal);
        private Stats _baseStats = new Stats(1, 2, 3, 4, 5, 6);
        private List<EvolutionCriteria> _evolutionCriterias = new List<EvolutionCriteria>();
        private double? _maleFactor = 0.6;

        private MoveSetBuilder _moveSetBuilder = new MoveSetBuilder();

        public SpeciesBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            _number = 128;
            _name = "Tauros";
            _type = new Typing(PokemonType.Normal);
            _baseStats = new Stats(1, 2, 3, 4, 5, 6);
            _evolutionCriterias = new List<EvolutionCriteria>();
            _maleFactor = 0.6;
        }

        public SpeciesBuilder WithNumber(uint number)
        {
            _number = number;
            return this;
        }

        public SpeciesBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public SpeciesBuilder WithType(Typing type)
        {
            _type = type;
            return this;
        }

        public SpeciesBuilder WithBaseStats(Stats baseStats)
        {
            _baseStats = baseStats;
            return this;
        }

        public SpeciesBuilder WithMaleFactor(double? maleFactor)
        {
            _maleFactor = maleFactor;
            return this;
        }

        public SpeciesBuilder WithEvolutionCriterias(List<EvolutionCriteria> evolutionCriterias)
        {
            _evolutionCriterias = evolutionCriterias;
            return this;
        }

        public SpeciesBuilder AddMove(MoveByLevel moveByLevel)
        {
            _moveSetBuilder.AddMove(moveByLevel);
            return this;
        }

        public SpeciesBuilder ResetMoves()
        {
            _moveSetBuilder.ResetMoves();
            return this;
        }

        public SpeciesBuilder WithMoveSet(List<MoveByLevel> moveSet)
        {
            _moveSetBuilder.ResetMoves();
            moveSet.ForEach(x => _moveSetBuilder.AddMove(x));
            return this;
        }

        public Species Build()
        {
            var species = new Species(
                _number,
                _name,
                _type,
                _baseStats,
                _maleFactor,
                _evolutionCriterias,
                _moveSetBuilder.Build()
            );
            Reset();
            return species;
        }

        public SpeciesDynamoDb ConvertToDynamoDb(Species species)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Species, SpeciesDynamoDb>();
                cfg.CreateMap<Typing, TypingDynamoDb>();
                cfg.CreateMap<Stats, StatsDynamoDb>();
                cfg.CreateMap<Move, MoveDynamoDb>();
                cfg.CreateMap<MoveByLevel, MoveByLevelDynamoDb>();
                cfg.CreateMap<EvolutionCriteria, EvolutionCriteriaDynamoDb>();
                cfg.CreateMap<Level?, uint?>().ConvertUsing((src, dest) =>
                {
                    if (src is null)
                    {
                        return null;
                    }
                    else
                    {
                        return src.Value;
                    }
                });
            });

            config.AssertConfigurationIsValid();
            var mapper = config.CreateMapper();

            return mapper.Map<SpeciesDynamoDb>(species);
        }
    }
}