using System.Linq;
using System.Threading;
using FluentAssertions;
using TechChallenge.Domain.Common;
using TechChallenge.Domain.LeadManagement;
using TechChallenge.Infrastructure.Repositories;
using Xunit;

namespace TechChallenge.Tests.Infrastructure
{
    public class LeadRepositoryTests
    {
        public LeadRepositoryTests()
        {
            DomainEvents.Init();
        }

        [Theory]
        [InlineData(LeadStatus.Accepted, LeadStatus.New)]
        [InlineData(LeadStatus.New, LeadStatus.Accepted)]
        public async void Predicate_Filters_Correctly(LeadStatus predicateStatus, LeadStatus shouldNotContainStatus)
        {
            // Arrange 
            var leadRepository = new LeadRepository(StubDbContextFactory.Create());

            // Act
            var leads = await leadRepository.FindByStatus(predicateStatus, new CancellationToken());

            // Assert
            leads.Should().NotContain(lead => lead.Status.Equals(shouldNotContainStatus));
        }

        [Fact]
        public async void SaveChangesValue()
        {
            // Arrange 
            var leadRepository = new LeadRepository(StubDbContextFactory.Create());
            var leads = await leadRepository.FindByStatus(LeadStatus.New, new CancellationToken());
            var leadsList = leads.ToList();
            leadsList.Should().NotBeEmpty();
            var inputLead = leadsList.First();
            inputLead.Accept();

            // Act
            await leadRepository.SaveAsync(inputLead, new CancellationToken());
            var outputLead = await leadRepository.FindById(inputLead.Id, new CancellationToken());

            // Assert
            outputLead.Status.Should().Be(LeadStatus.Accepted);
        }
    }
}
