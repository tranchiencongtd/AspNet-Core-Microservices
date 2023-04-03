using Contracts.Common.Interfaces;
using Customer.API.Persistence;

namespace Customer.API.Repositories.Interfaces
{
  public interface ICustomerRepository : IRepositoryQueryBase<Entities.Customer, int, CustomerContext>
  {
    Task<IEnumerable<Entities.Customer>> GetCustomersAsync();
    Task<Entities.Customer> GetCustomerByIdAsync(long id);
    Task<Entities.Customer> GetCustomerByEmailAsync(string email);
    Task<Entities.Customer> GetCustomerByUserNameAsync(string email);
    Task CreateCustomerAsync(Entities.Customer customer);
    Task UpdateCustomerAsync(Entities.Customer customer);
    Task DeleteCustomerAsync(long id);
  }
}
