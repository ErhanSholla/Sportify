
using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Core.Repository;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Data.Repositories;

namespace Catalog.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Catalog.API", Version = "v1" }); });
            /* Register AutoMapper
             * Scan the catalog.Application assembly because that's where ProductMapping is store
             * and automatically register all automapper mapping profiles found here
             * 
             * This Allows us to inject Imapper anywhere in the application  and use predefined mapping configurations between 
             */
            builder.Services.AddAutoMapper(typeof(ProductMappingProfile).Assembly);

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetAllBrandsQuery).Assembly));

            // Register Application Servies
            builder.Services.AddScoped<ICatalogContext, CatalogContext>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ITypesRepo, TypRepository>();
            builder.Services.AddScoped<IBrandRepo, BrandRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
