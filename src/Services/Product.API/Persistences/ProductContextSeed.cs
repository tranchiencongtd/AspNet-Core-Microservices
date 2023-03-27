using Product.API.Entities;
using ILogger = Serilog.ILogger;

namespace Product.API.Persistences
{
  public class ProductContextSeed
  {
    public static async Task SeedProductAsync(ProductContext productContext, ILogger logger)
    {
      if (!productContext.Products.Any())
      {
        productContext.AddRange(GetCatalogProduct());
        await productContext.SaveChangesAsync();
        logger.Information("Seeded data for ProductDB associated with context {DbContextName}", nameof(Product));
      }
    }

    private static IEnumerable<CatalogProduct> GetCatalogProduct()
    {
      return new List<CatalogProduct>
        {
            new()
            {
                No = "Latus",
                Name = "Esprit",
                Summary= "test1",
                Description = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit",
                Price = (decimal)17789.23
            },
            new()
            {
                No = "Cadillac",
                Name = "CTS",
                Summary= "test2",
                Description = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit",
                Price = (decimal)2332.23
            }
        };
    }
  }
}
