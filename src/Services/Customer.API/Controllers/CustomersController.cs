using Customer.API.Services.Interfaces;

namespace Customer.API.Controllers
{
  public static class CustomersController
  {
    public static void MapCustomersApi(this WebApplication app)
    {
      app.MapGet("/api/customers/{userName}", async (string userName, ICustomerService customerService) =>
      {
        var result = await customerService.GetCustomerByUserNameAsync(userName);
        return result != null ? Results.Ok(result) : Results.NoContent();
      });
    }
  }
}
