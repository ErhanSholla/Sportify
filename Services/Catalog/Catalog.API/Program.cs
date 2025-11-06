
using Catalog.API.Extension;
using Catalog.Infrastructure.Data;
using System.Threading.Tasks;

namespace Catalog.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            var connectionString = builder.Configuration.GetSection("DatabaseSettings:ConnectionString").Value;
            Console.WriteLine($"[DEBUG] MongoDB Connection String: {connectionString}");


            builder.Services.AddApiVersioningConfig().AddSwaggerConfig().AddApplicationServices();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ICatalogContext>();
                await ((CatalogContext)context).SeedAsync();
            }

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
