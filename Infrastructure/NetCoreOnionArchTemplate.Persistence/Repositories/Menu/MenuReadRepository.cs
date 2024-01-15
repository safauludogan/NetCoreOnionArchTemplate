using NetCoreOnionArchTemplate.Application.Repositories;
using NetCoreOnionArchTemplate.Domain.Entities;
using NetCoreOnionArchTemplate.Persistence.Context;

namespace NetCoreOnionArchTemplate.Persistence.Repositories
{
	public class MenuReadRepository : ReadRepository<Menu>, IMenuReadRepository
	{
		public MenuReadRepository(DataContext context) : base(context)
		{
		}
	}
}
