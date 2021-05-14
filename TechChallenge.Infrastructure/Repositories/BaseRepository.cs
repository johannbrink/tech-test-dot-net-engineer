using TechChallenge.Domain.Common;

namespace TechChallenge.Infrastructure.Repositories
{
    public class BaseRepository
    {
        protected static void DispatchEvents(AggregateRoot aggregateRoot)
        {
            if (aggregateRoot == null)
                return;

            foreach (var domainEvent in aggregateRoot.DomainEvents)
            {
                DomainEvents.Dispatch(domainEvent);
            }

            aggregateRoot.ClearEvents();
        }
    }
}
