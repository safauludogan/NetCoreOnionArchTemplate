namespace NetCoreOnionArchTemplate.Application.Features.Queries.Role.GetRoles
{
	public class GetAllRolesQueryResponse
	{
        public IDictionary<int, string> Roles { get; set; }
        public int TotalRoleCount { get; set; }
    }
}