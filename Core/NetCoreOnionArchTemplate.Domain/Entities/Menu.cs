using NetCoreOnionArchTemplate.Domain.Entities.Common;

namespace NetCoreOnionArchTemplate.Domain.Entities
{
	public class Menu : BaseEntity
	{
        public string Name { get; set; }
		public ICollection<Endpoint> Endpoints { get; set; }

	}
}
