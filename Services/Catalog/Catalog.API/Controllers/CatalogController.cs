using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Resposes;
using Catalog.Core.Specification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    public class CatalogController : ApiController
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetAllProducts")]
        [ProducesResponseType(typeof(Pagination<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Pagination<ProductResponse>>> GetAllProducts([FromQuery] CatalogSpecifcationParam catalogSpecifcationParam)
        {
            var query = new GetAllProductsQuery(catalogSpecifcationParam);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]{productName}", Name = "GetProductsByName")]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductsByName(string productName)
        {
            var query = new GetProductsByNameQuery(productName);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("[action]/{id}", Name = "GetProductsById")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponse>> GetProductById(string id)
        {
            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("[action]/{brand}", Name = "GetProductByBrandName")]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductByBrandName(string brand)
        {
            var query = new GetProductsByBrandQuery(brand);
            var result = _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllBrands")]
        [ProducesResponseType(typeof(IEnumerable<BrandResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<BrandResponse>>> GetAllBrands()
        {
            var query = new GetAllBrandsQuery();
            var result = _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllTypes")]
        [ProducesResponseType(typeof(IEnumerable<TypeResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<TypeResponse>>> GetAllTypes()
        {
            var query = new GetAllTypesQuery();
            var result = _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Route("CreateProduct")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> CreateProduct([FromBody] CreateProductCommand productCommand)
        {
            var result = await _mediator.Send(productCommand);

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> UpdateProduct([FromBody] UpdateProductCommand updateProductCommand)
        {
            var result = _mediator.Send(updateProductCommand);
            return Ok(result);
        }
    }
}
