using AutoMapper;
using PokemonSystem.BillsPC.Domain.SpeciesAggregate;
using PokemonSystem.BillsPC.Infra.DatabaseDtos;
using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;

namespace PokemonSystem.BillsPC.Infra.Adapters
{
    public class SpeciesProfile : Profile
    {
        public SpeciesProfile()
        {
            CreateMap<SpeciesDynamoDb, Species>()
                .ForMember(dest => dest.DomainEvents, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<TypingDynamoDb, Typing>()
                .ReverseMap();
            CreateMap<uint?, Level?>()
                .ConstructUsing((source, context) => source is null ? null : new Level(source.Value))
                .ForMember(dest => dest!.Value, opt => opt.Ignore())
                .ReverseMap()
                .ConstructUsing((source, context) => source is null ? null : source.Value);
            CreateMap<uint, Level>()
                .ConstructUsing((source, context) => new Level(source))
                .ForMember(dest => dest!.Value, opt => opt.Ignore())
                .ReverseMap()
                .ConstructUsing((source, context) => source.Value);
        }
    }

    public class DbSpeciesAdapter : IDbSpeciesAdapter
    {
        private readonly IMapper _mapper;

        public DbSpeciesAdapter()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SpeciesProfile>();
            });
            config.AssertConfigurationIsValid();

            _mapper = config.CreateMapper();
        }

        public Species ConvertToModel(SpeciesDynamoDb speciesDynamoDb)
        {
            return _mapper.Map<Species>(speciesDynamoDb);
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
    }
}
