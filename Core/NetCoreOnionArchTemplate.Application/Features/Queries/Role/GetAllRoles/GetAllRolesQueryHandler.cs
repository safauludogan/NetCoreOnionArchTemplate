
using MediatR;
using NetCoreOnionArchTemplate.Application.Abstractions.Services;

namespace NetCoreOnionArchTemplate.Application.Features.Queries.Role.GetRoles
{
	public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQueryRequest, GetAllRolesQueryResponse>
	{
		private readonly IRoleService _roleService;

		public GetAllRolesQueryHandler(IRoleService roleService)
		{
			_roleService = roleService;
		}

		public async Task<GetAllRolesQueryResponse> Handle(GetAllRolesQueryRequest request, CancellationToken cancellationToken)
		{
			var roles = _roleService.GetAllRoles();
			return new() { Roles = roles, TotalRoleCount = roles.Count };
		}
	}
}
