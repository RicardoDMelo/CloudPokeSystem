using MediatR;
using PokemonSystem.BillsPC.Domain.SpeciesAggregate;
using PokemonSystem.Common.Enums;
using PokemonSystem.Common.SeedWork.Domain;
using PokemonSystem.Common.ValueObjects;

namespace PokemonSystem.BillsPC.Domain.PokemonAggregate
{
    public record PokemonCreated(Guid Id, string? Nickname, Species Species, Gender Gender) : INotification;
    public record PokemonLeveled(Guid Id, Level Level, Stats stats) : INotification;
    public record PokemonEvolved(Guid Id, Species Species, Stats Stats) : INotification;
    public record PokemonLearnedMove(Guid Id, List<Move> LearntMoves) : INotification;

    public class Pokemon : StreamAggregate<Guid>
    {
        public Pokemon(Guid id, IEnumerable<INotification> @events) : base(id)
        {
            foreach (var @event in @events)
            {
                AddDomainEvent(@event);
                switch (@event)
                {
                    case PokemonCreated created:
                        Apply(created);
                        break;
                    case PokemonLeveled leveled:
                        Apply(leveled);
                        break;
                    case PokemonEvolved evolved:
                        Apply(evolved);
                        break;
                    case PokemonLearnedMove learned:
                        Apply(learned);
                        break;
                }
            }
        }

        public Pokemon(Guid id, string? nickname, Species species, Gender gender) : base(id)
        {
            var @event = new PokemonCreated(id, nickname, species, gender);
            AddDomainEvent(@event);
            Apply(@event);
        }

        public void RaiseLevel(Guid id, Level level, Stats stats)
        {
            var @event = new PokemonLeveled(id, level, stats);
            AddDomainEvent(@event);
            Apply(@event);
        }

        public void Evolve(Guid id, Species species, Stats stats)
        {
            var @event = new PokemonEvolved(id, species, stats);
            AddDomainEvent(@event);
            Apply(@event);
        }

        public void Teach(Guid id, List<Move> learntMoves)
        {
            var @event = new PokemonLearnedMove(id, learntMoves);
            AddDomainEvent(@event);
            Apply(@event);
        }

        public string? Nickname { get; protected set; }
        public Gender Gender { get; protected set; }
        public Species PokemonSpecies { get; protected set; }
        public Level? Level { get; protected set; }
        public LearntMoves? LearntMoves { get; protected set; }
        public Stats? Stats { get; protected set; }

        private void Apply(PokemonCreated @event)
        {
            Id = @event.Id;
            Nickname = @event.Nickname;
            PokemonSpecies = @event.Species;
            Gender = @event.Gender;
            LearntMoves = null;
            Stats = null;
            Level = null;
        }

        private void Apply(PokemonLeveled @event)
        {
            Level = @event.Level;
            Stats = @event.stats;
        }

        private void Apply(PokemonEvolved @event)
        {
            PokemonSpecies = @event.Species;
            Stats = @event.Stats;
        }

        private void Apply(PokemonLearnedMove @event)
        {
            LearntMoves = new LearntMoves(@event.LearntMoves);
        }
    }
}
