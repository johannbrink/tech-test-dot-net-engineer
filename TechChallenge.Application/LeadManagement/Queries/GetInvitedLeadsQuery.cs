using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechChallenge.Domain.LeadManagement;

namespace TechChallenge.Application.LeadManagement.Queries
{
    public class GetInvitedLeadsQuery : IRequest<IList<InvitedLeadListDto>>
    {
    }

    public class GetInvitedLeadsQueryHandler : IRequestHandler<GetInvitedLeadsQuery, IList<InvitedLeadListDto>>
    {
        private readonly ILeadRepository _leadRepository;

        public GetInvitedLeadsQueryHandler(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository;
        }

        public async Task<IList<InvitedLeadListDto>> Handle(GetInvitedLeadsQuery request, CancellationToken cancellationToken)
        {
            var leads = await _leadRepository.FindByStatus(LeadStatus.New, cancellationToken);
            return leads
                .Select(FromLead)
                .ToList();
        }

        private static InvitedLeadListDto FromLead(Lead lead)
        {
            return new InvitedLeadListDto()
            {
                Id = lead.Id,
                Category = lead.Category.Name,
                Contact = lead.Contact.FirstName,
                Description = lead.Description,
                Suburb = lead.Suburb.Name,
                PostCode = lead.Suburb.PostCode,
                Price = lead.Price.Value,
                DateCreated = lead.CreatedAt
            };
        }
    }
}
