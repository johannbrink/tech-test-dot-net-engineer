using TechChallenge.Domain.Common;

namespace TechChallenge.Domain.DomainEvents
{
    public class LeadDeclinedEvent : IDomainEvent
    {
        public int LeadId { get; }

        public LeadDeclinedEvent(int leadId)
        {
            LeadId = leadId;
        }
    }
}