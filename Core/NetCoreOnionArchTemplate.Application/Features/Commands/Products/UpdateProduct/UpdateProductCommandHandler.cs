using MediatR;
using Microsoft.Extensions.Logging;
using NetCoreOnionArchTemplate.Application.Interfaces.AutoMapper;
using NetCoreOnionArchTemplate.Application.Interfaces.UnitOfWorks;
using NetCoreOnionArchTemplate.Domain.Entities;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Products.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly ILogger<UpdateProductCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateProductCommandHandler(ILogger<UpdateProductCommandHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            Product? product = await _unitOfWork.GetReadRepository<Product>().GetByIdAsync(request.Id);
            if (product != null)
            {
                //product = _mapper.Map<Product, UpdateProductCommandRequest>(request);
                product.Stock = request.Stock;
                product.Price = request.Price;
                product.Name = request.Name;
                var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
                if (result)
                {
                    _logger.LogInformation("Product Güncellendi...");
                    return new() { IsSucceeded = false };
                }
                else
                    _logger.LogInformation("Product Güncellenemedi...");
            }
            else
                _logger.LogInformation("Product is null...");
            return new() { IsSucceeded = false };
        }
    }
}
