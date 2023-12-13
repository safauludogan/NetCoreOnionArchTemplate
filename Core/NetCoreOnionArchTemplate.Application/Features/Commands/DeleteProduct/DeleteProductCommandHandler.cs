using MediatR;
using NetCoreOnionArchTemplate.Application.Repositories;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        private readonly IProductWriteRepository _productWriteRepository;

        public DeleteProductCommandHandler(IProductWriteRepository productWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
        }

        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWriteRepository.RemoveAsync(request.Id);
            var result = await _productWriteRepository.SaveAsync();
            return new DeleteProductCommandResponse(result > 0 ? true : false);
        }
    }
}
