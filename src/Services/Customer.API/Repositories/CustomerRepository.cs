using Contracts.Common.Interfaces;
using Customer.API.Persistence;
using Customer.API.Repositories.Interfaces;
using Infrastructure.Common;
using System.Linq.Expressions;

namespace Customer.API.Repositories
{
  public class CustomerRepository : RepositoryBaseAsync<Entities.Customer, int, CustomerContext>, ICustomerRepository
  {
    public CustomerRepository(CustomerContext dbContext, IUnitOfWork<CustomerContext> unitOfWork) : base(dbContext, unitOfWork)
    {
    }

    public Task CreateCustomerAsync(Entities.Customer customer)
    {
      throw new NotImplementedException();
    }

    public Task DeleteCustomerAsync(long id)
    {
      throw new NotImplementedException();
    }

    public Task<Entities.Customer> GetCustomerByEmailAsync(string email)
    {
      throw new NotImplementedException();
    }

    public Task<Entities.Customer> GetCustomerByIdAsync(long id)
    {
      throw new NotImplementedException();
    }

    public Task<Entities.Customer> GetCustomerByUserNameAsync(string email)
    {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<Entities.Customer>> GetCustomersAsync()
    {
      throw new NotImplementedException();
    }

    public Task UpdateCustomerAsync(Entities.Customer customer)
    {
      throw new NotImplementedException();
    }
  }
}
