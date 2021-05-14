using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechChallenge.Application.Common;

namespace TechChallenge.Application.LeadManagement.Commands
{
    public class AcceptLeadCommand : IRequest<CommandStatus>
    {
        public int LeadId { get; set; }
    }

    public class AcceptLeadCommandHandler : IRequestHandler<AcceptLeadCommand, CommandStatus>
    {
        private readonly ILeadRepository _leadRepository;

        public AcceptLeadCommandHandler(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository;
        }

        public async Task<CommandStatus> Handle(AcceptLeadCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var lead = await _leadRepository.FindById(request.LeadId, cancellationToken);
                lead.Accept();
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
