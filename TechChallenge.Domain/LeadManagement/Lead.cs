using System;
using TechChallenge.Domain.Common;
using TechChallenge.Domain.DomainEvents;
using TechChallenge.Domain.ValueObjects;

namespace TechChallenge.Domain.LeadManagement
{
    public class Lead: AggregateRoot
    {
        public LeadStatus Status { get; private set; }
        public int SuburbId { get; set; }
        public Suburb Suburb { get; }
        public int CategoryId { get; set; }
        public Category Category { get; }
        public Contact Contact { get; }
        public Amount Price { get; private set; }
        public string Description { get;  }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get;  }

        private Lead()
        {
        }

        public Lead(Category category, Suburb suburb, Contact contact, Amount price)
        {
            Category = category;
            Suburb = suburb;
            Contact = contact;
            Price = price;
            Status = LeadStatus.New;
        }

        public void Accept()
        {
            if (Price.Value > 500)
                Price = Price.Discount(10);
            Status = LeadStatus.Accepted;
            AddDomainEvent(new LeadAcceptedEvent(Id));
        }

        public void Decline()
        {
            Status = LeadStatus.Declined;
            AddDomainEvent(new LeadDeclinedEvent(Id));
        }

    }
}
