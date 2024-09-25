using MediatR;
using NetCoreOnionArchTemplate.Application.Abstractions.RedisCache;
using NetCoreOnionArchTemplate.Application.RequestParameters;

namespace NetCoreOnionArchTemplate.Application.Features.Queries.Products.GetAllProduct
{
    public class GetAllProductQueryRequest : IRequest<GetAllProductQueryResponse> , ICacheableQuery
    {
        public string CacheKey => "GetAllProducts";

        public double CacheTime => 5;

        public Pagination _Pagination { get; set; }
        public GetAllProductQueryRequest(Pagination Pagination)
        {
            _Pagination = Pagination;
        }
    }
}
