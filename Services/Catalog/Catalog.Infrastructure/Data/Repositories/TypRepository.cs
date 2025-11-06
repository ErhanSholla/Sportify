using AutoMapper;
using Catalog.Core.Entities;
using Catalog.Core.Repository;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.Repositories;
public class TypRepository : ITypesRepo
{
    private readonly ICatalogContext _catalogContext;

    public TypRepository(ICatalogContext catalogContext, IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(catalogContext, nameof(catalogContext));
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));
        _catalogContext = catalogContext;
        _mapper = mapper;
    }

    public IMapper _mapper { get; }

    public async Task<IEnumerable<ProductType>> GetAllTypes()
    {
        var document = await _catalogContext.Types.Find(p => true).ToListAsync();
        return _mapper.Map<IEnumerable<ProductType>>(document);
    }
}

