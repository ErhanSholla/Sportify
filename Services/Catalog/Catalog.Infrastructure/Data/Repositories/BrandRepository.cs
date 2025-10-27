using Catalog.Core.Entities;
using Catalog.Core.Repository;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.Repositories;
public class BrandRepository : IBrandRepo
{
    private readonly ICatalogContext _catalogContext;

    public BrandRepository(ICatalogContext catalogContext)
    {
        ArgumentNullException.ThrowIfNull(catalogContext, nameof(catalogContext));
        _catalogContext = catalogContext;
    }
    public async Task<IEnumerable<ProductBrand>> GetAllBrands()
    {
        return await _catalogContext.Brands.Find(b => true).ToListAsync();
    }

}
