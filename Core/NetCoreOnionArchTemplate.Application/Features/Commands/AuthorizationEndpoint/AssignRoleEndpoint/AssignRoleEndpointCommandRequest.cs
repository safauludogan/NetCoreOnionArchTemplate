using MediatR;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.AuthorizationEndpoint.AssignRoleEndpoint
{
	public class AssignRoleEndpointCommandRequest : IRequest<AssignRoleEndpointCommandResponse>
	{
        public string[] Roles{ get; set; }
        public string Code { get; set; }
    }
}
