
using MediatR;
using NetCoreOnionArchTemplate.Application.Abstractions.Services;
using NetCoreOnionArchTemplate.Application.DTOs.Role;

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
			var roles = await _roleService.GetAllRoles(page: request.Page, size: request.Size);

			List<GetAllRoles> getAllRoles = new();
			foreach (var role in roles)
			{
				getAllRoles.Add(new GetAllRoles
				{
					Id = role.Id,
					Name = role.Name
				});
			}

			return new() { Roles = getAllRoles, TotalRoleCount = roles.Count };
		}
	}
}
