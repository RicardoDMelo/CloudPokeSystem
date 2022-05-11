using AutoMapper;
using PokemonSystem.Common.Enums;
using PokemonSystem.Common.ValueObjects;
using PokemonSystem.Learning.Domain.SpeciesAggregate;
using PokemonSystem.Learning.Infra.DatabaseDtos;

namespace PokemonSystem.Learning.Infra.Adapters
{
    public class SpeciesProfile : Profile
    {
        public SpeciesProfile()
        {
            CreateMap<SpeciesDynamoDb, Species>()
                .ForMember(dest => dest.DomainEvents, opt => opt.Ignore());
            CreateMap<MoveByLevelDynamoDb, MoveByLevel>()
                .ForMember(dest => dest.DomainEvents, opt => opt.Ignore());
            CreateMap<MoveDynamoDb, Move>();
            CreateMap<uint?, Level?>()
                .ConstructUsing(source => source == null ? null : new Level(source.Value))
                .ForMember(dest => dest!.Value, opt => opt.Ignore());
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
            return new Species(source.Id,
                               source.Name,
                               context.Mapper.Map<List<MoveByLevel>>(source.MoveSet));
        }
        private SpeciesDynamoDb ConvertToSpeciesDynamoDb(Species source, ResolutionContext context)
        {
            return new SpeciesDynamoDb()
            {
                Id = source.Id,
                Name = source.Name,
                MoveSet = context.Mapper.Map<List<MoveByLevelDynamoDb>>(source.MoveSet)
            };
        }
        private Move ConvertToMove(MoveDynamoDb source, ResolutionContext context)
        {
            return new Move(source.Name, (PokemonType)source.Type, (MoveCategory)source.Category, source.Power, source.Accuracy, source.PP);
        }
        private MoveDynamoDb ConvertToMoveDynamoDb(Move source, ResolutionContext context)
        {
            return new MoveDynamoDb()
            {
                Name = source.Name,
                Type = (short)source.Type,
                Category = (short)source.Category,
                Power = source.Power,
                Accuracy = source.Accuracy,
                PP = source.PP
            };
        }
        private MoveByLevel ConvertToMoveByLevel(MoveByLevelDynamoDb source, ResolutionContext context)
        {
            return new MoveByLevel(
                context.Mapper.Map<Level>(source.Level),
                context.Mapper.Map<Move>(source.Move));
        }
        private MoveByLevelDynamoDb ConvertToMoveByLevelDynamoDb(MoveByLevel source, ResolutionContext context)
        {
            return new MoveByLevelDynamoDb()
            {
                Level = context.Mapper.Map<uint>(source.Level),
                Move = context.Mapper.Map<MoveDynamoDb>(source.Move)
            };
        }
    }
}
