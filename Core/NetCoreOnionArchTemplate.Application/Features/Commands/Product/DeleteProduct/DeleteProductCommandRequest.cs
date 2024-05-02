using MediatR;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Product.DeleteProduct
{
    public class DeleteProductCommandRequest : IRequest<DeleteProductCommandResponse>
    {
        public Guid Id { get; set; }
        public DeleteProductCommandRequest(Guid id)
        {
            Id = id;
        }
    }
}
