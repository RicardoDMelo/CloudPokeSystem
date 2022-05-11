using AutoMapper;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Learning.Domain.PokemonAggregate;
using PokemonSystem.Learning.Infra.DatabaseDtos;

namespace PokemonSystem.Learning.Infra.Adapters
{
    public class PokemonProfile : Profile
    {
        public PokemonProfile()
        {
            CreateMap<PokemonDynamoDb, Pokemon>()
                .ForMember(dest => dest.DomainEvents, opt => opt.Ignore());
            CreateMap<List<MoveDynamoDb>, LearntMoves>()
                .ConvertUsing((src, dest, context) =>
                {
                    dest = new LearntMoves();
                    foreach (var moveDynamoDb in src)
                    {
                        var move = context.Mapper.Map<Move>(moveDynamoDb);
                        dest.AddMove(move);
                    }
                    return dest;
                });
        }
    }

    public class PokemonAdapter : IPokemonAdapter
    {
        private readonly IMapper _mapper;

        public PokemonAdapter()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SpeciesProfile>();
                cfg.AddProfile<PokemonProfile>();
            });
            config.AssertConfigurationIsValid();

            _mapper = config.CreateMapper();
        }

        public PokemonDynamoDb ConvertToDto(Pokemon pokemon)
        {
            return _mapper.Map<PokemonDynamoDb>(pokemon);
        }

        public Pokemon ConvertToModel(PokemonDynamoDb pokemonDynamoDb)
        {
            return _mapper.Map<Pokemon>(pokemonDynamoDb);
        }
    }
}
