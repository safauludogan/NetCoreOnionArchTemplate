using NetCoreOnionArchTemplate.Application.DTOs.Role;

namespace NetCoreOnionArchTemplate.Application.Features.Queries.Role.GetRoles
{
	public class GetAllRolesQueryResponse
	{
        public List<GetAllRoles> Roles { get; set; }
        public int TotalRoleCount { get; set; }
    }
}