using Catalog.Application.Resposes;
using Catalog.Core.Specification;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetAllBrandsQuery : IRequest<IList<BrandResponse>>
    {


    }
}
