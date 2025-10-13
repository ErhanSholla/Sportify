using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Resposes;
using Catalog.Core.Entities;


namespace Catalog.Application.Mappers
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductResponse>().ReverseMap();
            CreateMap<ProductBrand, BrandResponse>().ReverseMap();
            CreateMap<ProductType, TypeResponse>().ReverseMap();
            CreateMap<Product, CreateProductCommand>().ReverseMap();
        }
    }
}
