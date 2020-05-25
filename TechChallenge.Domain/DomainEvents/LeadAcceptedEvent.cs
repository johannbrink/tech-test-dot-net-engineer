using TechChallenge.Domain.Common;

namespace TechChallenge.Domain.DomainEvents
{
    public class LeadAcceptedEvent : IDomainEvent
    {
        public int LeadId { get; }

        public LeadAcceptedEvent(int leadId)
        {
            LeadId = leadId;
        }
    }
}