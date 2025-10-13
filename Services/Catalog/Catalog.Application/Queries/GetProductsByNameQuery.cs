using Catalog.Application.Resposes;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetProductsByNameQuery : IRequest<IList<ProductResponse>>
    {
        public string ProductName { get; init; }

        public GetProductsByNameQuery(string name)
        {
            ProductName = name;
        }
    }
}
