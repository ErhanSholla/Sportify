using AutoMapper;
using Catalog.Core.Entities;
using Catalog.Core.Repository;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.Repositories;
public class BrandRepository : IBrandRepo
{
    private readonly ICatalogContext _catalogContext;
    private readonly IMapper _mapper;

    public BrandRepository(ICatalogContext catalogContext, IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(catalogContext, nameof(catalogContext));
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));
        _catalogContext = catalogContext;
        _mapper = mapper;
    }
    public async Task<IEnumerable<ProductBrand>> GetAllBrands()
    {
        var docs = await _catalogContext.Brands.Find(b => true).ToListAsync();

        return _mapper.Map<IEnumerable<ProductBrand>>(docs);
    }

}
