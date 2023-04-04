using Customer.API.Entities;
using Customer.API.Repositories.Interfaces;
using Customer.API.Services.Interfaces;

namespace Customer.API.Services
{
  public class CustomerService : ICustomerService
  {
    private readonly ICustomerRepository _repository;
    public CustomerService(ICustomerRepository repository)
    {
      _repository = repository;
    }


    public async Task<IResult> GetCustomerByUserNameAsync(string userName)
    {
      var result = await _repository.GetCustomerByUserNameAsync(userName);
      return Results.Ok(result);  
    }

    public async Task<IResult> GetCustomersAsync()
    {
      var result = await _repository.GetCustomersAsync();
      return Results.Ok(result);
    }

    public async Task<IResult> CreateCustomerAsync(Entities.Customer customer)
    {
      await _repository.CreateAsync(customer);
      await _repository.SaveChangesAsync();
      return Results.Ok(customer);
    }

    public async Task<IResult> DeleteCustomerAsync(int id)
    {
      var customer = await _repository.GetCustomerByIdAsync(id);
      if (customer == null) 
        return Results.NoContent();

      await _repository.DeleteAsync(customer);
      await _repository.SaveChangesAsync();
      return Results.NoContent();
    }

    public async Task<IResult> UpdateCustomerAsync(Entities.Customer customer)
    {
      await _repository.UpdateAsync(customer);
      await _repository.SaveChangesAsync();
      return Results.Ok(customer);
    }
  }
}
