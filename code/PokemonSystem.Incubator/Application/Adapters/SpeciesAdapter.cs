using AutoMapper;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Incubator.Domain.SpeciesAggregate;
using PokemonSystem.Incubator.Infra.Database;

namespace PokemonSystem.PokedexInjector
{
    internal class SpeciesAdapter : ISpeciesAdapter
    {
        private readonly IMapper _mapper;

        public SpeciesAdapter()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SpeciesDynamoDb, Species>()
                    .ForMember(x => x.DomainEvents, x => x.Ignore());
                cfg.CreateMap<TypingDynamoDb, Typing>();
                cfg.CreateMap<StatsDynamoDb, Stats>();
                cfg.CreateMap<MoveDynamoDb, Move>();
                cfg.CreateMap<MoveByLevelDynamoDb, MoveByLevel>()
                    .ForMember(x => x.DomainEvents, x => x.Ignore());
                cfg.CreateMap<EvolutionCriteriaDynamoDb, EvolutionCriteria>()
                    .ForMember(x => x.DomainEvents, x => x.Ignore());
                cfg.CreateMap<uint, Level>().ConvertUsing(f => new Level(f));
            });
            config.AssertConfigurationIsValid();

            _mapper = config.CreateMapper();
        }

        public Species ConvertToDto(SpeciesDynamoDb speciesDynamoDb)
        {
            return _mapper.Map<Species>(speciesDynamoDb);
        }
    }
}
