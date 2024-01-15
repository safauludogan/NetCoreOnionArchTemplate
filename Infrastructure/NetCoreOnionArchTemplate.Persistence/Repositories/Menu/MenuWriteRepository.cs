using NetCoreOnionArchTemplate.Application.Repositories;
using NetCoreOnionArchTemplate.Domain.Entities;
using NetCoreOnionArchTemplate.Persistence.Context;

namespace NetCoreOnionArchTemplate.Persistence.Repositories
{
	public class MenuWriteRepository : WriteRepository<Menu>, IMenuWriteRepository
	{
		public MenuWriteRepository(DataContext context) : base(context)
		{
		}
	}
}
