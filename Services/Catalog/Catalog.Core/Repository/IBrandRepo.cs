using Catalog.Core.Entities;

namespace Catalog.Core.Repository;
public interface IBrandRepo
{
    Task<IEnumerable<ProductBrand>> GetAllBrands();

}

