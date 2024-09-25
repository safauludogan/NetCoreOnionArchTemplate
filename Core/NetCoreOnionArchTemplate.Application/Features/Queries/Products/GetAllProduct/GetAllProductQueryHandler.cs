using MediatR;
using Microsoft.Extensions.Logging;
using NetCoreOnionArchTemplate.Application.Abstractions.UnitOfWorks;
using NetCoreOnionArchTemplate.Application.DTOs.Products;
using NetCoreOnionArchTemplate.Domain.Entities;

namespace NetCoreOnionArchTemplate.Application.Features.Queries.Products.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllProductQueryHandler> _logger;
        public GetAllProductQueryHandler(ILogger<GetAllProductQueryHandler> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get all products");
            //var totalCount = _unitOfWork.GetReadRepository<Product>().GetAll(tracking: false).Count();
            var products = _unitOfWork.GetReadRepository<Product>().GetAll(tracking: false)
                .Take(500000)
            /*.Skip(request._Pagination.Page * request._Pagination.Size).Take(request._Pagination.Size)*/.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                /*  p.Stock,
                 p.Price,
                p.CreatedDate,
                 p.UpdatedDate*/
            }).ToList();

            return new GetAllProductQueryResponse()
            {
                Products = products,
                TotalCount = 1
            };
        }
    }
}
