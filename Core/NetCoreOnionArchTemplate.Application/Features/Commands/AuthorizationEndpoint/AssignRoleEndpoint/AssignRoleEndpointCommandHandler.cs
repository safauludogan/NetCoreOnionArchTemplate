using MediatR;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.AuthorizationEndpoint.AssignRoleEndpoint
{
	public class AssignRoleEndpointCommandHandler : IRequestHandler<AssignRoleEndpointCommandRequest, AssignRoleEndpointCommandResponse>
	{
		public async Task<AssignRoleEndpointCommandResponse> Handle(AssignRoleEndpointCommandRequest request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
