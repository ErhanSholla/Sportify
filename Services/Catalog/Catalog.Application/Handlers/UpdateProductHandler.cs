using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Core.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.UpdateProduct(new Core.Entities.Product
            {
                Id = request.Id,
                Name = request.Name,
                Summary = request.Summary,
                Description = request.Description,
                ImageFile = request.ImageFile,
                Price = request.Price,
                Brand = request.Brand,
                Type = request.Type
            });

            return result;
        }
    }
}
