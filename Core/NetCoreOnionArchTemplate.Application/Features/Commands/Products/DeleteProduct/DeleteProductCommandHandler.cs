using MediatR;
using NetCoreOnionArchTemplate.Application.Interfaces.UnitOfWorks;
using NetCoreOnionArchTemplate.Application.Repositories;
using NetCoreOnionArchTemplate.Domain.Entities;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Products.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _unitOfWork.GetWriteRepository<Product>().RemoveAsync(request.Id);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            return new DeleteProductCommandResponse(result);
        }
    }
}
