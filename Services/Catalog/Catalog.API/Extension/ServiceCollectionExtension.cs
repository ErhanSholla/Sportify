using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Core.Repository;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Data.Repositories;
using Catalog.Infrastructure.Documents;
using Catalog.Infrastructure.Mappers;
using Catalog.Infrastructure.Sorting;

namespace Catalog.API.Extension
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Auto Mapper
            services.AddAutoMapper(typeof(ProductMappingProfile).Assembly, typeof(ProductDocumentProfile).Assembly);

            // Mediatr
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(GetAllBrandsQuery).Assembly);
            });

            // Catalog and repositories
            services.AddScoped<ICatalogContext, CatalogContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ITypesRepo, TypRepository>();
            services.AddScoped<IBrandRepo, BrandRepository>();


            // Sort Strategies
            services.AddScoped<IMongoSortStrategy<ProductDocument>, MongoNameSortStrategy>();
            services.AddScoped<IMongoSortStrategy<ProductDocument>, MongoPriceAscSortStrategy>();
            services.AddScoped<IMongoSortStrategy<ProductDocument>, MongoPriceDescSortStrategy>();
            services.AddScoped<MongoSortStrategyFactory<ProductDocument>>();


            return services;
        }

        public static IServiceCollection AddApiVersioningConfig(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
            });
            return services;
        }

        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Catalog.API",
                    Version = "v1"
                }
                );
            });
            return services;
        }

    }
}