using NetCoreOnionArchTemplate.Domain.Entities.Common;

namespace NetCoreOnionArchTemplate.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string Description { get; set; }
        public string Address { get; set; }
        public ICollection<Product> Products { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
