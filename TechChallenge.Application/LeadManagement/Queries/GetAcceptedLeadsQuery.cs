using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechChallenge.Domain.LeadManagement;

namespace TechChallenge.Application.LeadManagement.Queries
{
    public class GetAcceptedLeadsQuery : IRequest<IList<AcceptedLeadListDto>>
    {
    }

    public class GetAcceptedLeadsQueryHandler : IRequestHandler<GetAcceptedLeadsQuery, IList<AcceptedLeadListDto>>
    {
        private readonly ILeadRepository _leadRepository;

        public GetAcceptedLeadsQueryHandler(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository;
        }

        public async Task<IList<AcceptedLeadListDto>> Handle(GetAcceptedLeadsQuery request, CancellationToken cancellationToken)
        {
            var leads = await _leadRepository.FindByStatus(LeadStatus.Accepted, cancellationToken);
            return leads
                .Select(FromLead)
                .ToList();
        }

        private static AcceptedLeadListDto FromLead(Lead lead)
        {
            return new AcceptedLeadListDto()
            {
                Id = lead.Id,
                Category = lead.Category.Name,
                Contact = lead.Contact.FullName,
                Description = lead.Description,
                Suburb = lead.Suburb.Name,
                PostCode = lead.Suburb.PostCode,
                Price = lead.Price.Value,
                Email = lead.Contact.EmailAddress,
                Phone = lead.Contact.PhoneNumber,
                DateCreated = lead.CreatedAt
            };
        }
    }
}
