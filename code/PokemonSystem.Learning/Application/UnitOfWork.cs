using MediatR;
using PokemonSystem.Common.SeedWork;

namespace PokemonSystem.Learning.Application
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationContext _applicationContext;
        private readonly IMediator _mediator;

        public UnitOfWork(IApplicationContext applicationContext, IMediator mediator)
        {
            _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public void Commit()
        {
            var entities = _applicationContext.Entities;
            _applicationContext.Reset();

            foreach (var entity in entities)
            {
                foreach(var domainEvent in entity.DomainEvents)
                {
                    _mediator.Publish(domainEvent);
                }                
            }
        }
    }
}
