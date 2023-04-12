using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using Serilog;


namespace Ordering.Infrastructure.Persistence
{
  public class OrderContextSeed
  {
    private readonly ILogger _logger;
    private readonly OrderContext _orderContext;

    public OrderContextSeed(ILogger logger, OrderContext orderContext)
    {
      _logger = logger;
      _orderContext = orderContext;
    }

    public async Task InitializeAsync()
    {
      try
      {
        if(_orderContext.Database.IsSqlServer()) await _orderContext.Database.MigrateAsync();
      }
      catch (Exception ex)
      {
        _logger.Error(ex, "An error ccurred while initialzing the database");
        throw;
      }
    }

    public async Task SeedAsync()
    {
      try
      {
        await TrySeedAsync();
        await _orderContext.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        _logger.Error(ex, "An error occurred while seeding the database");
        throw;
      }
    }

    public async Task TrySeedAsync()
    {
      if(!_orderContext.Orders.Any())
      {
        await _orderContext.Orders.AddRangeAsync(
          new Order
          {
            UserName = "customer1",
            FirstName= "Test",
            LastName= "Test",
            EmailAddress = "email@gmail.com",
            ShippingAddress = "HN",
            InvoiceAddress = "HN",
            TotalPrice = 100
          } );
      }
    }
  }
}
