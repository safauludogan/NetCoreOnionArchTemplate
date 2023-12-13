using MediatR;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Product.DeleteProduct
{
    public class DeleteProductCommandRequest : IRequest<DeleteProductCommandResponse>
    {
        public int Id { get; set; }
        public DeleteProductCommandRequest(int id)
        {
            Id = id;
        }
    }
}
