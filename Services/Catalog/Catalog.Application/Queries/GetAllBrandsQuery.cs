using Catalog.Application.Resposes;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetAllBrandsQuery : IRequest<IList<BrandResponse>>
    {


    }
}
