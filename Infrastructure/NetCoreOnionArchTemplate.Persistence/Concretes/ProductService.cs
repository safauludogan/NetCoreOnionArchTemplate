using NetCoreOnionArchTemplate.Application.Abstractions;
using NetCoreOnionArchTemplate.Domain.Entities;

namespace NetCoreOnionArchTemplate.Persistence.Concretes
{
    public class ProductService : IProductService
    {
        public List<Product> GetProducts()
        => new()
        {
            new(){Id=1,Name="Product 1",Price = 100,Stock=10},
            new(){Id=2,Name="Product 2",Price = 110,Stock=80},
            new(){Id=3,Name="Product 3",Price = 140,Stock=500},
            new(){Id=4,Name="Product 4",Price = 120,Stock=420},
            new(){Id=5,Name="Product 5",Price = 10,Stock=3},
        };
    }
}
