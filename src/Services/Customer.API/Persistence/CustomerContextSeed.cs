using Microsoft.EntityFrameworkCore;

namespace Customer.API.Persistence;

public static class CustomerContextSeed
{
  public static IHost SeedCustomerData(this IHost host)
  {
    using var scope = host.Services.CreateScope();
    var customerContext = scope.ServiceProvider.GetRequiredService<CustomerContext>();
    customerContext.Database.MigrateAsync().GetAwaiter().GetResult();

    CreateCustomer(customerContext, "congtc1", "tran", "chiencong", "congtc1@gmail.com").GetAwaiter().GetResult();
    CreateCustomer(customerContext, "congtc2", "tran", "chiencong2", "congtc2@gmail.com").GetAwaiter().GetResult();

    return host;
  }

  private static async Task CreateCustomer(CustomerContext customerContext, string userName, string firstName, string lastName, string email)
  {
    var customer = await customerContext.Customers
             .SingleOrDefaultAsync(x => x.UserName.Equals(userName) || x.Email.Equals(email));

    if (customer == null)
    {
      var newCustomer = new Entities.Customer
      {
        UserName = userName,
        FirstName = firstName,
        LastName = lastName,
        Email = email
      };

      await customerContext.Customers.AddAsync(newCustomer);
      await customerContext.SaveChangesAsync();
    }
  }
}

