using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechChallenge.Application.Common;

namespace TechChallenge.Application.LeadManagement.Commands
{
    public class DeclineLeadCommand : IRequest<CommandStatus>
    {
        public int LeadId { get; set; }
    }

    public class DeclineLeadCommandHandler : IRequestHandler<DeclineLeadCommand, CommandStatus>
    {
        private readonly ILeadRepository _leadRepository;

        public DeclineLeadCommandHandler(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository;
        }

        public async Task<CommandStatus> Handle(DeclineLeadCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var lead = await _leadRepository.FindById(request.LeadId, cancellationToken);
                lead.Decline();
                await _leadRepository.SaveAsync(lead, cancellationToken);
                return CommandStatus.Success;
            }
            catch
            {
                return CommandStatus.Failed;
            }
        }
    }
}
