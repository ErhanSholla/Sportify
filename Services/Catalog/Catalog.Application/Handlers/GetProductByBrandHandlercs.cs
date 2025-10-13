

using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Resposes;
using Catalog.Core.Repository;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetProductByBrandHandlercs : IRequestHandler<GetProductsByBrandQuery, IList<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByBrandHandlercs(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IList<ProductResponse>> Handle(GetProductsByBrandQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetProductsByBrandName(request.BrandName);
            var productResponseList = ProductMapper.Mapper.Map<IList<ProductResponse>>(productList);
            return productResponseList;
        }
    }
}
