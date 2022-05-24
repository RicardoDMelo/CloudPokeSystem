using System.Text.Json;

namespace PokemonSystem.Common.SeedWork.Domain
{
    public  class EventRecord<T>
    {
        protected EventRecord()
        {

        }
        public EventRecord(object data, T streamId, int streamPosition)
        {
            Id = Guid.NewGuid();
            Type = data.GetType().Name;
            StreamId = streamId;
            StreamPosition = streamPosition;
            Timestamp = DateTime.Now;
            Data = JsonSerializer.Serialize(data);
        }

        public Guid Id { get; private set; }
        public string Type { get; private set; }
        public T StreamId { get; private set; }
        public int StreamPosition { get; private set; }
        public DateTime Timestamp { get; private set; }
        public string Data { get; private set; }
    }
}
