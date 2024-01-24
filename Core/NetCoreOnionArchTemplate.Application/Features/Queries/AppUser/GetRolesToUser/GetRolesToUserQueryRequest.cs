using MediatR;

namespace NetCoreOnionArchTemplate.Application.Features.Queries.AppUser.GetRolesToUser
{
	public class GetRolesToUserQueryRequest : IRequest<GetRolesToUserQueryResponse>
	{
        public string userId{ get; set; }
    }
}