using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Resposes;
using Catalog.Core.Repository;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetAllTypesHandler : IRequestHandler<GetAllTypesQuery, IList<TypeResponse>>
    {
        private readonly ITypesRepo _typesRepository;

        public GetAllTypesHandler(ITypesRepo typesRepository)
        {
            _typesRepository = typesRepository;
        }
        public async Task<IList<TypeResponse>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
        {
            var typeList = await _typesRepository.GetAllTypes();
            var typeResponseList = ProductMapper.Mapper.Map<IList<TypeResponse>>(typeList);
            return typeResponseList;
        }
    }
}
