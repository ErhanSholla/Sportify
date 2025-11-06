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
            // Document -> Domain and Vice versa
            CreateMap<ProductDocument, Product>().ReverseMap();
            CreateMap<ProductBrandDocument, ProductBrand>().ReverseMap();
            CreateMap<ProductTypeDocument, ProductType>().ReverseMap();
        }
    }
}
