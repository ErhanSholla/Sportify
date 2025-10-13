using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Application.Resposes;
using Catalog.Core.Entities;
using Catalog.Core.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = ProductMapper.Mapper.Map<Product>(request);
            if (productEntity == null)
            {
                throw new ApplicationException("This is an issue with mapping while creating");
            }
            var newProduct = await _productRepository.CreateProduct(productEntity);
            return ProductMapper.Mapper.Map<ProductResponse>(newProduct);
        }
    }
}
