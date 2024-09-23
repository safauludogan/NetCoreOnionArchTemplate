using MediatR;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Auth.AssignRoleToUser
{
	public class AssignRoleToUserCommandRequest : IRequest<AssignRoleToUserCommandResponse>
	{
		public string UserId { get; set; }
		public string[] Roles { get; set; }
	}
}