using MediatR;
using NetCoreOnionArchTemplate.Application.Abstractions.Services;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Role.DeleteRole
{
	public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommandRequest, DeleteRoleCommandResponse>
	{

		private readonly IRoleService _roleService;

		public DeleteRoleCommandHandler(IRoleService roleService)
		{
			_roleService = roleService;

		}
		public async Task<DeleteRoleCommandResponse> Handle(DeleteRoleCommandRequest request, CancellationToken cancellationToken)
		{
			var result = await _roleService.DeleteRoleAsync(request.Id);
			return new()
			{
				Succeeded = result
			};
		}
	}
}
