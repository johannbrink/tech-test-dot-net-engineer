using FluentAssertions;
using TechChallenge.Domain.DomainEvents;
using TechChallenge.Domain.LeadManagement;
using TechChallenge.Domain.ValueObjects;
using Xunit;

namespace TechChallenge.Tests.Domain
{
    public class LeadTests
    {
        [Theory]
        [InlineData(10, 10)]
        [InlineData(500, 500)]
        [InlineData(501, 450.9)]
        public void Accept_Sets_State_And_Price(double price, double expectedPrice)
        {
            // Arrange
            var lead = CreateLead(price);
            lead.Status.Should().Be(LeadStatus.New);
            
            // Act
            lead.Accept();

            // Assert
            lead.Status.Should().Be(LeadStatus.Accepted);
            lead.Price.Value.Should().Be(expectedPrice);
            lead.DomainEvents.Should()
                .NotBeEmpty()
                .And.HaveCount(1)
                .And.ContainItemsAssignableTo<LeadAcceptedEvent>();
        }

        [Fact]
        public void Decline_Sets_State()
        {
            // Arrange
            var lead = CreateLead(1);
            lead.Status.Should().Be(LeadStatus.New);
            
            // Act
            lead.Decline();

            // Assert
            lead.Status.Should().Be(LeadStatus.Declined);
        }

        private static Lead CreateLead(double priceAmount)
        {
            var suburb = new Suburb("Sydney", "2000");
            var category = new Category("Plumbing", 0);
            var lead = new Lead(
                category,
                suburb,
                new Contact
                (
                    "first",
                    "last",
                    "000",
                    "a@b.c"),
                new Amount(priceAmount));
            return lead; 
        }
    }
}
