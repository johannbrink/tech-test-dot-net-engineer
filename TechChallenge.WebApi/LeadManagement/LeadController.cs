using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.Application.Common;
using TechChallenge.Application.LeadManagement.Commands;
using TechChallenge.Application.LeadManagement.Queries;
using TechChallenge.WebApi.Common;

namespace TechChallenge.WebApi.LeadManagement
{
    [ApiController]
    [Route("[controller]")]
    public class LeadController : ApiController
    {

        [HttpGet]
        [Route("invited")]
        public async Task<IActionResult> InvitedLeads()
        {

            var response = await Mediator.Send(new GetInvitedLeadsQuery());
            return Ok(response);
        }

        [HttpGet]
        [Route("accepted")]
        public async Task<IActionResult> AcceptedLeads()
        {

            var response = await Mediator.Send(new GetAcceptedLeadsQuery());
            return Ok(response);
        }

        [HttpPost]
        [Route("accept")]
        public async Task<IActionResult> Accept(AcceptLeadCommand acceptLeadCommand)
        {
            var response = await Mediator.Send(acceptLeadCommand);
            if (response == CommandStatus.Success)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost]
        [Route("decline")]
        public async Task<IActionResult> Decline(DeclineLeadCommand declineLeadCommand)
        {
            var response = await Mediator.Send(declineLeadCommand);
            if (response == CommandStatus.Success)
                return Ok();
            else
                return BadRequest();
        }
    }
}