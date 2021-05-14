using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechChallenge.Application.Interfaces;
using TechChallenge.Domain.LeadManagement;

namespace TechChallenge.Application.Services
{
    public class LeadRepository : BaseRepository, ILeadRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public LeadRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Lead> FindById(int id)
        {
            return await FindById(id, new CancellationToken());
        }

        public async Task<Lead> FindById(int id, CancellationToken cancellationToken)
        {
            var results = await _dbContext.Leads
                .Include(_ => _.Category)
                .Include(_ => _.Suburb)
                .ToListAsync(cancellationToken);
            return results.FirstOrDefault(x => x.Id.Equals(id));
        }

        public async Task<IEnumerable<Lead>> FindByStatus(LeadStatus leadStatus, CancellationToken cancellationToken)
        {
            var results = await _dbContext.Leads
                .Include(_ => _.Category)
                .Include(_ => _.Suburb)
                .ToListAsync(cancellationToken);
            return results.Where(x => x.Status.Equals(leadStatus));
        }

        public async Task<Lead> SaveAsync(Lead lead, CancellationToken cancellationToken)
        {
            if (lead.Id.Equals(0))
                _dbContext.Leads.Add(lead);
            else
                _dbContext.Leads.Attach(lead);
            await _dbContext.SaveChangesAsync(cancellationToken);
            DispatchEvents(lead);
            return lead;
        }
    }
}
