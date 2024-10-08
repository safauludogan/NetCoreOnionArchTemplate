﻿using MediatR;
using NetCoreOnionArchTemplate.Application.Repositories;

namespace NetCoreOnionArchTemplate.Application.Features.Queries.Products.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest, GetProductByIdQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetProductByIdQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.GetByIdAsync(request.Id, tracking: false);
            return new GetProductByIdQueryResponse() { Product = product };
        }
    }
}
