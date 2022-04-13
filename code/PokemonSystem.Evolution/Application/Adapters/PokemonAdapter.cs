using AutoMapper;
using PokemonSystem.Common.Enums;
using PokemonSystem.Common.Properties;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Evolution.Application.Commands;
using PokemonSystem.Evolution.Domain.PokemonAggregate;
using PokemonSystem.Evolution.Infra.DatabaseDtos;


namespace PokemonSystem.Evolution.Application.Adapters
{
    public class PokemonAdapter : IPokemonAdapter
    {
        private readonly IMapper _mapper;

        public PokemonAdapter()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SpeciesDynamoDb, Species>().ConstructUsing(ConvertToSpecies)
                    .IgnoreAllPropertiesWithAnInaccessibleSetter();
                cfg.CreateMap<StatsDynamoDb, Stats>().ConstructUsing(ConvertToStats)
                    .IgnoreAllPropertiesWithAnInaccessibleSetter();
                cfg.CreateMap<EvolutionCriteriaDynamoDb, EvolutionCriteria>().ConstructUsing(ConvertToEvolutionCriteria)
                    .IgnoreAllPropertiesWithAnInaccessibleSetter();
                cfg.CreateMap<uint, Level>().ConvertUsing(f => new Level(f));
            });
            config.AssertConfigurationIsValid();

            _mapper = config.CreateMapper();
        }


        public Pokemon ConvertToModel(GrantPokemonLevel grantPokemonLevel, SpeciesDynamoDb speciesDynamoDb)
        {
            return new Pokemon(_mapper.Map<Species>(speciesDynamoDb), _mapper.Map<Level>(grantPokemonLevel.LevelToGrow));
        }

        private Species ConvertToSpecies(SpeciesDynamoDb source, ResolutionContext context)
        {
            return new Species(source.Id,
                               source.Name,
                               context.Mapper.Map<Stats>(source.BaseStats),
                               context.Mapper.Map<List<EvolutionCriteria>>(source.EvolutionCriterias));
        }

        private Stats ConvertToStats(StatsDynamoDb source, ResolutionContext context)
        {
            return new Stats(source.HP, source.Attack, source.Defense, source.SpecialAttack, source.SpecialDefense, source.Speed);
        }

        private EvolutionCriteria ConvertToEvolutionCriteria(EvolutionCriteriaDynamoDb source, ResolutionContext context)
        {
            var evolutionType = (EvolutionType)source.EvolutionType;
            switch (evolutionType)
            {
                case EvolutionType.Level:
                    return EvolutionCriteria.CreateLevelEvolution(context.Mapper.Map<Level>(source.MinimumLevel), context.Mapper.Map<Species>(source.Species));
                case EvolutionType.Item:
                    return EvolutionCriteria.CreateItemEvolution(source.Item!, context.Mapper.Map<Species>(source.Species));
                case EvolutionType.Trading:
                    return EvolutionCriteria.CreateTradingEvolution(context.Mapper.Map<Species>(source.Species));
                default:
                    throw new ArgumentException(string.Format(Errors.WrongEnum, evolutionType));
            }
        }
    }
}
