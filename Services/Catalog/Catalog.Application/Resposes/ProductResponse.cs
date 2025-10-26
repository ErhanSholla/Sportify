﻿using Catalog.Core.Entities;

namespace Catalog.Application.Resposes;
public class ProductResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
    public string ImageFile { get; set; }
    public ProductBrand Brand { get; set; }
    public ProductType Type { get; set; }

    public decimal Price { get; set; }
}

