using Catalog.Core.Entities;

namespace Catalog.Core.Repository
{
    public interface ITypesRepo
    {
        Task<IEnumerable<ProductType>> GetAllTypes();

    }
}
