using Catalog.Application.Resposes;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetAllTypesQuery : IRequest<IList<TypeResponse>>
    {
    }
}
