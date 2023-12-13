using MediatR;
using NetCoreOnionArchTemplate.Application.Repositories;


namespace NetCoreOnionArchTemplate.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductWriteRepository _productWriteRepository;
        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
        }
        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product product = new()
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            };
            await _productWriteRepository.AddAsync(product);
            var response = new CreateProductCommandResponse();
            response.IsSuccess = await _productWriteRepository.SaveAsync() > 0 ? true : false;

            return response;
        }
    }
}
