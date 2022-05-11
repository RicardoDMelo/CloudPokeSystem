using AutoMapper;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Learning.Domain.SpeciesAggregate;
using PokemonSystem.Learning.Infra.DatabaseDtos;

namespace PokemonSystem.Tests.Learning.Builders
{
    public class SpeciesBuilder
    {
        private uint _number = 128;
        private string _name = "Tauros";

        private MoveSetBuilder _moveSetBuilder = new MoveSetBuilder();

        public SpeciesBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            _number = 128;
            _name = "Tauros";
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
                cfg.CreateMap<Move, MoveDynamoDb>();
                cfg.CreateMap<MoveByLevel, MoveByLevelDynamoDb>();
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