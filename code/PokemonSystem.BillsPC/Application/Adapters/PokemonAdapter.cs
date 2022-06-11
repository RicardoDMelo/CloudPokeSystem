using AutoMapper;
using PokemonSystem.BillsPC.Application.ViewModel;
using PokemonSystem.BillsPC.Domain.PokemonAggregate;

namespace PokemonSystem.BillsPC.Application.Adapters
{
    public class PokemonProfile : Profile
    {
        public PokemonProfile()
        {
            DisableConstructorMapping();
            CreateMap<Pokemon, PokemonDetail>()
                .ForMember(dest => dest.SpeciesId, opt => opt.MapFrom(src => src.PokemonSpecies.Id))
                .ForMember(dest => dest.SpeciesName, opt => opt.MapFrom(src => src.PokemonSpecies.Name))
                .ForMember(dest => dest.Type1, opt => opt.MapFrom(src => src.PokemonSpecies.Typing.Type1))
                .ForMember(dest => dest.Type2, opt => opt.MapFrom(src => src.PokemonSpecies.Typing.Type2 == null ? null : src.PokemonSpecies.Typing.Type2))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level!.Value))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
                .ForMember(dest => dest.LearntMoves, opt => opt.MapFrom(src => src.LearntMoves!.Values));
            CreateMap<Pokemon, PokemonLookup>()
                .ForMember(dest => dest.SpeciesId, opt => opt.MapFrom(src => src.PokemonSpecies.Id))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level!.Value))
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

        public PokemonDetail ConvertToDetailViewModel(Pokemon pokemon)
        {
            return _mapper.Map<PokemonDetail>(pokemon);
        }

        public PokemonLookup ConvertToLookupViewModel(Pokemon pokemon)
        {
            return _mapper.Map<PokemonLookup>(pokemon);
        }
    }
}
