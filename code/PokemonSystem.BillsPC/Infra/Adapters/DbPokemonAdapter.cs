using AutoMapper;
using MediatR;
using PokemonSystem.BillsPC.Domain.PokemonAggregate;
using PokemonSystem.Common.SeedWork.Domain;
using System.Text.Json;

namespace PokemonSystem.BillsPC.Infra.Adapters
{
    public class EventRecordProfile : Profile
    {
        public EventRecordProfile()
        {
            DisableConstructorMapping();
            CreateMap(typeof(EventRecord<>), typeof(EventRecordDb)).ReverseMap();
        }
    }

    public class DbPokemonAdapter : IDbPokemonAdapter
    {
        private readonly IMapper _mapper;

        public DbPokemonAdapter()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EventRecordProfile>();
            });
            config.AssertConfigurationIsValid();

            _mapper = config.CreateMapper();
        }

        public IEnumerable<EventRecordDb> ConvertToDto(Guid pokemonId, IEnumerable<INotification> notifications)
        {
            var eventRecords = notifications.Select((x, count) => new EventRecord<Guid>(x, pokemonId, count));
            return eventRecords.Select(x => _mapper.Map<EventRecordDb>(x));
        }

        public IEnumerable<INotification> ConvertToModel(IEnumerable<EventRecordDb> pokemonsDynamoDb)
        {
            var eventRecords = pokemonsDynamoDb.Select(x => _mapper.Map<EventRecord<Guid>>(x));
            foreach (var eventRecord in eventRecords)
            {
                switch (eventRecord.Type)
                {
                    case "PokemonCreated":
                        yield return JsonSerializer.Deserialize<PokemonCreated>(eventRecord.Data)!;
                        break;
                    case "PokemonLeveled":
                        yield return JsonSerializer.Deserialize<PokemonLeveled>(eventRecord.Data)!;
                        break;
                    case "PokemonEvolved":
                        yield return JsonSerializer.Deserialize<PokemonEvolved>(eventRecord.Data)!;
                        break;
                    case "PokemonLearnedMove":
                        yield return JsonSerializer.Deserialize<PokemonLearnedMove>(eventRecord.Data)!;
                        break;
                }
            }
        }
    }
}
