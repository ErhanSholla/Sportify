using Catalog.Application.Resposes;
using Catalog.Core.Specification;
using MediatR;

namespace Catalog.Application.Queries;
public class GetAllProductsQuery : IRequest<Pagination<ProductResponse>>
{
    public CatalogSpecifcationParam _catalogSpecifcationParam;

    public GetAllProductsQuery(CatalogSpecifcationParam catalogSpecifcationParam)
    {
        _catalogSpecifcationParam = catalogSpecifcationParam;
    }
}

