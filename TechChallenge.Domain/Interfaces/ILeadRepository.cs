using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TechChallenge.Domain.LeadManagement;

namespace TechChallenge.Domain.Interfaces
{
    public interface ILeadRepository
    {
        Task<Lead> FindById(int id);
        Task<Lead> FindById(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Lead>> FindByStatus(LeadStatus leadStatus, CancellationToken cancellationToken);
        Task<Lead> SaveAsync(Lead lead, CancellationToken cancellationToken);
    }
}