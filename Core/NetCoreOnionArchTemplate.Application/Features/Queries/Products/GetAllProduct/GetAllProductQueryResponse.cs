using NetCoreOnionArchTemplate.Application.DTOs.Products;

namespace NetCoreOnionArchTemplate.Application.Features.Queries.Products.GetAllProduct
{
    public class GetAllProductQueryResponse
    {
        public int TotalCount { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
