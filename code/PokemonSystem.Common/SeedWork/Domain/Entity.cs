using MediatR;

namespace PokemonSystem.Common.SeedWork.Domain
{
    public abstract class Entity : Entity<Guid>
    {
        public Entity()
        {
            _Id = Guid.NewGuid();
        }
    }

    public abstract class Entity<T> : IEntity
    {
        int? _requestedHashCode;
        protected T _Id;
        private List<INotification> _domainEvents = new List<INotification>();

        public virtual T Id
        {
            get
            {
                return _Id;
            }
            protected set
            {
                _Id = value;
            }
        }

        public List<INotification> DomainEvents => _domainEvents;

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents.Remove(eventItem);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity<T>))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (GetType() != obj.GetType())
                return false;

            Entity<T> item = (Entity<T>)obj;
            return item.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            if (!_requestedHashCode.HasValue)
                _requestedHashCode = Id.GetHashCode() ^ 31;
            return _requestedHashCode.Value;
        }

        public static bool operator ==(Entity<T> left, Entity<T> right)
        {
            if (Equals(left, null))
                return Equals(right, null);
            else
                return left.Equals(right);
        }

        public static bool operator !=(Entity<T> left, Entity<T> right)
        {
            return !(left == right);
        }
    }
}
