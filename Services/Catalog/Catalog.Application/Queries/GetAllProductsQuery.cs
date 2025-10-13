using Catalog.Application.Resposes;
using MediatR;

namespace Catalog.Application.Queries;
public class GetAllProductsQuery : IRequest<IList<ProductResponse>>
{

}

