using Microsoft.EntityFrameworkCore;
using TechChallenge.Domain.LeadManagement;
using TechChallenge.Domain.ValueObjects;
using TechChallenge.Infrastructure.Persistence;

namespace TechChallenge.Tests.Infrastructure
{
    public static class StubDbContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("HipagesInMemory");
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            SeedDbContext(dbContext);
            return dbContext;
        }
        private static void SeedDbContext(ApplicationDbContext dbContext)
        {
            var suburb = new Suburb("Sydney", "2000");
            var category = new Category("Plumbing", 0);
            var category2 = new Category("SubPlumbing", 1);
            var lead = new Lead(
                category2,
                suburb,
                new Contact
                (
                    "first",
                    "last",
                    "000",
                    "a@b.c"),
                new Amount(1));
            var lead2 = new Lead(
                category2,
                suburb,
                new Contact
                (
                    "first2",
                    "last2",
                    "000",
                    "a@b.c"),
                new Amount(2));
            lead2.Accept();
            dbContext.Suburbs.Add(suburb);
            dbContext.Categories.Add(category);
            dbContext.Categories.Add(category2);
            dbContext.Leads.Add(lead);
            dbContext.Leads.Add(lead2);
            dbContext.SaveChanges();
        }


    }
}