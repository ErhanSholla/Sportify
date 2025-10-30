using Catalog.Core.Entities;
using Catalog.Core.Repository;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.Repositories;
public class TypRepository : ITypesRepo
{
    private readonly ICatalogContext _catalogContext;

    public TypRepository(ICatalogContext catalogContext)
    {
        ArgumentNullException.ThrowIfNull(catalogContext, nameof(catalogContext));
        _catalogContext = catalogContext;
    }
    public async Task<IEnumerable<ProductType>> GetAllTypes()
    {
        return await _catalogContext.Types.Find(p => true).ToListAsync();
    }
}

