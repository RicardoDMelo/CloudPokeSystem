using AutoMapper;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Learning.Domain.PokemonAggregate;
using PokemonSystem.Learning.Domain.SpeciesAggregate;
using PokemonSystem.Learning.Infra.DatabaseDtos;
using PokemonSystem.Tests.ValueObjects;

namespace PokemonSystem.Tests.Learning.Builders
{
    public class PokemonBuilder
    {
        private Guid _pokemonId = Guid.Empty;
        private Species? _species = null;
        private Level? _level = null;

        private SpeciesBuilder _speciesBuilder = new SpeciesBuilder();

        public PokemonBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            _pokemonId = Guid.NewGuid();
            _species = _speciesBuilder.Build();
            _level = Levels.One;
        }

        public PokemonBuilder WithPokemonId(Guid pokemonId)
        {
            _pokemonId = pokemonId;
            return this;
        }

        public PokemonBuilder WithSpecies(Species species)
        {
            _species = species;
            return this;
        }

        public PokemonBuilder WithLevel(Level level)
        {
            _level = level;
            return this;
        }

        public Pokemon Build()
        {
            var pokemon = new Pokemon(_pokemonId, _species!, _level!);
            Reset();
            return pokemon;
        }

        public PokemonDynamoDb ConvertToDynamoDb(Pokemon pokemon)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Pokemon, PokemonDynamoDb>();
                cfg.CreateMap<Species, SpeciesDynamoDb>();
                cfg.CreateMap<Move, MoveDynamoDb>();
                cfg.CreateMap<MoveByLevel, MoveByLevelDynamoDb>();
                cfg.CreateMap<LearntMoves, List<MoveDynamoDb>>()
                    .ConvertUsing((src, dest, context) =>
                    {
                        return src.Values
                            .Select(x => context.Mapper.Map<MoveDynamoDb>(x))
                            .ToList();
                    });
                cfg.CreateMap<Level, uint>().ConvertUsing((src, dest) => src.Value);
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

            return mapper.Map<PokemonDynamoDb>(pokemon);
        }
    }
}