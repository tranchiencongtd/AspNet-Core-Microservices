namespace Customer.API.Services.Interfaces
{
  public interface ICustomerService
  {
    Task<IResult> GetCustomerByUserNameAsync(string userName);
    Task<IResult> GetCustomersAsync();
    Task<IResult> CreateCustomerAsync(Entities.Customer customer);
    Task<IResult> UpdateCustomerAsync(Entities.Customer customer);
    Task<IResult> DeleteCustomerAsync(int id);
  }
}
