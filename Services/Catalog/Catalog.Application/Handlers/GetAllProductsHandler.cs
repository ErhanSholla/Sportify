using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Resposes;
using Catalog.Core.Repository;
using Catalog.Core.Specification;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, Pagination<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public async Task<Pagination<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetAllProducts(request._catalogSpecifcationParam);
            var productResponseList = ProductMapper.Mapper.Map<Pagination<ProductResponse>>(productList);
            return productResponseList;
        }
    }
}
