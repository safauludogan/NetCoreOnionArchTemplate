using NetCoreOnionArchTemplate.Application.Repositories;
using NetCoreOnionArchTemplate.Domain.Entities;
using NetCoreOnionArchTemplate.Persistence.Context;

namespace NetCoreOnionArchTemplate.Persistence.Repositories
{
    public class EndpointReadRepository : ReadRepository<Endpoint>, IEndpointReadRepository
	{
		public EndpointReadRepository(DataContext context) : base(context)
		{
		}
	}
}
