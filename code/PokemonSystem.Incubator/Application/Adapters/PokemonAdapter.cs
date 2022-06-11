using AutoMapper;
using PokemonSystem.Incubator.Application.ViewModel;
using PokemonSystem.Incubator.Domain.PokemonAggregate;

namespace PokemonSystem.Incubator.Application.Adapters
{
    public class PokemonProfile : Profile
    {
        public PokemonProfile()
        {
            DisableConstructorMapping();
            CreateMap<Pokemon, PokemonLookup>()
                .ForMember(dest => dest.SpeciesId, opt => opt.MapFrom(src => src.PokemonSpecies.Id))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.LevelToGrow!.Value))
                .ForMember(dest => dest.Name, opt => opt.MapFrom((src, dest) =>
                {
                    if (string.IsNullOrEmpty(src.Nickname))
                    {
                        return src.PokemonSpecies.Name;
                    }
                    else
                    {
                        return $"{src.Nickname} - {src.PokemonSpecies.Name}";
                    }
                }));
        }
    }
    public class PokemonAdapter : IPokemonAdapter
    {
        private readonly IMapper _mapper;

        public PokemonAdapter()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PokemonProfile>();
            });
            config.AssertConfigurationIsValid();

            _mapper = config.CreateMapper();
        }

        public PokemonLookup ConvertToLookupViewModel(Pokemon pokemon)
        {
            return _mapper.Map<PokemonLookup>(pokemon);
        }
    }
}
