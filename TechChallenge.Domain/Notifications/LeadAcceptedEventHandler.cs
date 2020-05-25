using TechChallenge.Domain.Common;
using TechChallenge.Domain.DomainEvents;

namespace TechChallenge.Domain.Notifications
{
    public class LeadAcceptedEventHandler : IHandler<LeadAcceptedEvent>
    {
        public async void Handle(LeadAcceptedEvent domainEvent)
        {
            //Send email
        }
    }
}
