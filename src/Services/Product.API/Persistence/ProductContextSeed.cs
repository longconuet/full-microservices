using Product.API.Entities;
using ILogger = Serilog.ILogger;

namespace Product.API.Persistence
{
    public static class ProductContextSeed
    {
        public static async Task SeedProductAsync(ProductDbContext context, ILogger logger)
        {
            if (!context.Products.Any())
            {
                context.AddRange(GetCatalogProducts());
                await context.SaveChangesAsync();
                logger.Information($"Seeded data for Product Db with context {nameof(ProductDbContext)}");
            }
        }

        private static IEnumerable<CatalogProduct> GetCatalogProducts()
        {
            return
            [
                new()
                {
                    No = "Lotus",
                    Name = "Esprit",
                    Summary = "Summary Lorem ipsum 1",
                    Description = "Description Lorem ipsum 1",
                    Price = (decimal)14.34
                },
                new()
                {
                    No = "Cadillac",
                    Name = "Super car",
                    Summary = "Summary Lorem ipsum 1",
                    Description = "Description Lorem ipsum 1",
                    Price = (decimal)763.45
                }
            ];
        }
    }
}
