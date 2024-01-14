using MediatR;

namespace NetCoreOnionArchTemplate.Application.Features.Queries.Product.GetProductById
{
    public class GetProductByIdQueryRequest : IRequest<GetProductByIdQueryResponse>
    {
        public int Id { get; set; }

        public GetProductByIdQueryRequest(int id)
        {
            Id = id;
        }
    }
}
