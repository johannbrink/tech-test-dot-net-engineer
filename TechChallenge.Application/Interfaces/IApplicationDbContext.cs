using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechChallenge.Domain.LeadManagement;

namespace TechChallenge.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Lead> Leads { get; set; }
        DbSet<Suburb> Suburbs { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
