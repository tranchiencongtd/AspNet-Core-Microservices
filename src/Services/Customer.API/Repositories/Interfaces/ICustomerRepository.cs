using Contracts.Common.Interfaces;
using Customer.API.Persistence;

namespace Customer.API.Repositories.Interfaces
{
  public interface ICustomerRepository : IRepositoryBaseAsync<Entities.Customer, int, CustomerContext>
  {
    Task<IEnumerable<Entities.Customer>> GetCustomersAsync();
    Task<Entities.Customer?> GetCustomerByIdAsync(int id);
    Task<Entities.Customer?> GetCustomerByEmailAsync(string email);
    Task<Entities.Customer?> GetCustomerByUserNameAsync(string userName);
    Task CreateCustomerAsync(Entities.Customer customer);
    Task UpdateCustomerAsync(Entities.Customer customer);
    Task DeleteCustomerAsync(int id);
  }
}
