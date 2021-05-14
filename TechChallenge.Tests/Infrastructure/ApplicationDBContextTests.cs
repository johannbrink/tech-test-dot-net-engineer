using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TechChallenge.Infrastructure.Persistence;
using Xunit;

namespace TechChallenge.Tests.Infrastructure
{
    public class ApplicationDbContextTests
    {
        private readonly ApplicationDbContext _dbContext;

        public ApplicationDbContextTests()
        {
            _dbContext = StubDbContextFactory.Create();
        }

        [Fact]
        public void Can_Read_Categories()
        {
            var category = _dbContext.Categories.First();
            category.Should().NotBeNull();
        }

        [Fact]
        public void Can_Read_Categories_With_Parents()
        {
            var category = _dbContext.Categories.First(c => c.ParentId != 0);
            category.Should().NotBeNull();
        }

        [Fact]
        public void Can_Read_Leads_AllOwned_Properties()
        {
            var lead = _dbContext.Leads
                .Include(l => l.Suburb)
                .Include(l => l.Category)
                .First();
            lead.Should().NotBeNull();
            lead.Id.Should().NotBe(0);
            lead.Suburb.Should().NotBeNull();
            lead.Suburb.Name.Should().NotBeNull();
            lead.Suburb.PostCode.Should().NotBeNull();
            lead.Category.Should().NotBeNull();
            lead.Category.Name.Should().NotBeNull();
            lead.Contact.Should().NotBeNull();
            lead.Contact.FirstName.Should().NotBeNull();
            lead.Contact.LastName.Should().NotBeNull();
            lead.Contact.FullName.Should().NotBeNull();
            lead.Contact.EmailAddress.Should().NotBeNull();
            lead.Contact.PhoneNumber.Should().NotBeNull();
        }
        
        [Fact]
        public void Can_Read_Suburbs()
        {
            var suburb = _dbContext.Suburbs.First();
            suburb.Should().NotBeNull();
        }

    }
}
