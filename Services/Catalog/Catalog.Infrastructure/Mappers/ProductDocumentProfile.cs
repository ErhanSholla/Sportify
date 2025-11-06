using AutoMapper;
using Catalog.Core.Entities;
using Catalog.Infrastructure.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Mappers
{
    public class ProductDocumentProfile : Profile
    {
        public ProductDocumentProfile()
        {
            // Entity -> Domain Vice Versa
            CreateMap<ProductBrand, ProductBrandDocument>().ReverseMap();
            CreateMap<Product, ProductDocument>().ReverseMap();
            CreateMap<ProductType, ProductTypeDocument>().ReverseMap();
        }
    }
}
