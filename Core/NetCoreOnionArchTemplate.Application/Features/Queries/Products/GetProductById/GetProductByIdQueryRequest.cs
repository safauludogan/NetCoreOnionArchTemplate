using MediatR;

namespace NetCoreOnionArchTemplate.Application.Features.Queries.Product.GetProductById
{
    public class GetProductByIdQueryRequest : IRequest<GetProductByIdQueryResponse>
    {
        public Guid Id { get; set; }

        public GetProductByIdQueryRequest(Guid id)
        {
            Id = id;
        }
    }
}
