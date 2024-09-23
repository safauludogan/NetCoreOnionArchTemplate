using NetCoreOnionArchTemplate.Application.Repositories;
using NetCoreOnionArchTemplate.Domain.Entities;
using NetCoreOnionArchTemplate.Persistence.Context;

namespace NetCoreOnionArchTemplate.Persistence.Repositories
{
    public class EndpointWriteRepository : WriteRepository<Endpoint>, IEndpointWriteRepository
	{
		public EndpointWriteRepository(DataContext context) : base(context)
		{
		}
	}
}
