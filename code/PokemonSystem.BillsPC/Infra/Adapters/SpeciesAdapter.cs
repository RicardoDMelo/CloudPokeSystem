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

    public class SpeciesAdapter : ISpeciesAdapter
    {
        private readonly IMapper _mapper;

        public SpeciesAdapter()
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

        public SpeciesDynamoDb ConvertToDto(Species species)
        {
            return _mapper.Map<SpeciesDynamoDb>(species);
        }

        private Species ConvertToSpecies(SpeciesDynamoDb source, ResolutionContext context)
        {
            return new Species(source.Id, source.Name);
        }
        private SpeciesDynamoDb ConvertToSpeciesDynamoDb(Species source, ResolutionContext context)
        {
            return new SpeciesDynamoDb()
            {
                Id = source.Id,
                Name = source.Name,
            };
        }
    }
}
