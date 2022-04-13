using AutoMapper;
using PokemonSystem.Common.Enums;
using PokemonSystem.Common.Properties;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Incubator.Domain.SpeciesAggregate;
using PokemonSystem.Incubator.Infra.DatabaseDtos;

namespace PokemonSystem.Incubator.Application.Adapters
{
    public class SpeciesAdapter : ISpeciesAdapter
    {
        private readonly IMapper _mapper;

        public SpeciesAdapter()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SpeciesDynamoDb, Species>().ConstructUsing(ConvertToSpecies)
                    .IgnoreAllPropertiesWithAnInaccessibleSetter();
                cfg.CreateMap<TypingDynamoDb, Typing>().ConstructUsing(ConvertToTyping)
                    .IgnoreAllPropertiesWithAnInaccessibleSetter();
                cfg.CreateMap<StatsDynamoDb, Stats>().ConstructUsing(ConvertToStats)
                    .IgnoreAllPropertiesWithAnInaccessibleSetter();
                cfg.CreateMap<MoveDynamoDb, Move>().ConstructUsing(ConvertToMove)
                    .IgnoreAllPropertiesWithAnInaccessibleSetter();
                cfg.CreateMap<MoveByLevelDynamoDb, MoveByLevel>().ConstructUsing(ConvertToMoveByLevel)
                    .IgnoreAllPropertiesWithAnInaccessibleSetter();
                cfg.CreateMap<EvolutionCriteriaDynamoDb, EvolutionCriteria>().ConstructUsing(ConvertToEvolutionCriteria)
                    .IgnoreAllPropertiesWithAnInaccessibleSetter();
                cfg.CreateMap<uint, Level>().ConvertUsing(f => new Level(f));
            });
            config.AssertConfigurationIsValid();

            _mapper = config.CreateMapper();
        }

        public Species ConvertToModel(SpeciesDynamoDb speciesDynamoDb)
        {
            return _mapper.Map<Species>(speciesDynamoDb);
        }

        private Species ConvertToSpecies(SpeciesDynamoDb source, ResolutionContext context)
        {
            var a = context.Mapper.Map<List<EvolutionCriteriaDynamoDb>, List<EvolutionCriteria>>(source.EvolutionCriterias);
            return new Species(source.Id,
                               source.Name,
                               context.Mapper.Map<Typing>(source.Typing),
                               context.Mapper.Map<Stats>(source.BaseStats),
                               source.MaleFactor,
                               a,
                               context.Mapper.Map<List<MoveByLevel>>(source.MoveSet));
        }

        private Typing ConvertToTyping(TypingDynamoDb source, ResolutionContext context)
        {
            if (source.Type2 is null)
            {
                return new Typing((PokemonType)source.Type1);
            }
            else
            {
                return new Typing((PokemonType)source.Type1, (PokemonType)source.Type2);
            }
        }

        private Stats ConvertToStats(StatsDynamoDb source, ResolutionContext context)
        {
            return new Stats(source.HP, source.Attack, source.Defense, source.SpecialAttack, source.SpecialDefense, source.Speed);
        }

        private Move ConvertToMove(MoveDynamoDb source, ResolutionContext context)
        {
            return new Move(source.Name, (PokemonType)source.Type, (MoveCategory)source.Category, source.Power, source.Accuracy, source.PP);
        }

        private MoveByLevel ConvertToMoveByLevel(MoveByLevelDynamoDb source, ResolutionContext context)
        {
            return new MoveByLevel(
                context.Mapper.Map<Level>(source.Level),
                context.Mapper.Map<Move>(source.Move));
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
