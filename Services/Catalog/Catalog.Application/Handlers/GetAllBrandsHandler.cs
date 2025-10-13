using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Resposes;
using Catalog.Core.Repository;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery, IList<BrandResponse>>
    {
        private readonly IBrandRepo _brandRepository;

        public GetAllBrandsHandler(IBrandRepo brandRepository)
        {
            _brandRepository = brandRepository;
        }
        public Task<IList<BrandResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var brandList = _brandRepository.GetAllBrands();
            var brandResponseList = ProductMapper.Mapper.Map<IList<BrandResponse>>(brandList);
            return Task.FromResult(brandResponseList);
        }
    }
}
