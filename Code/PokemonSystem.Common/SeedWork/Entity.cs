using System;
using System.Collections.Generic;

namespace PokemonSystem.Common.SeedWork
{
    public abstract class Entity
    {
        int? _requestedHashCode;
        Guid _Id;
        private List<INotification> _domainEvents;

        public virtual Guid Id
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
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            if (_domainEvents is null) return;
            _domainEvents.Remove(eventItem);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
                return false;
            if (Object.ReferenceEquals(this, obj))
                return true;
            if (this.GetType() != obj.GetType())
                return false;

            Entity item = (Entity)obj;
            return item.Id == this.Id;
        }

        public override int GetHashCode()
        {
            if (!_requestedHashCode.HasValue)
                _requestedHashCode = this.Id.GetHashCode() ^ 31;
            return _requestedHashCode.Value;
        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null));
            else
                return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}
