using NetCoreOnionArchTemplate.Domain.Entities;

namespace NetCoreOnionArchTemplate.Application.Abstractions
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }
}
