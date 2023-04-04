using Contracts.Common.Interfaces;
using Customer.API.Persistence;
using Customer.API.Repositories.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Customer.API.Repositories
{
  public class CustomerRepository : RepositoryBaseAsync<Entities.Customer, int, CustomerContext>, ICustomerRepository
  {
    public CustomerRepository(CustomerContext dbContext, IUnitOfWork<CustomerContext> unitOfWork) : base(dbContext, unitOfWork)
    {
    }

    public async Task CreateCustomerAsync(Entities.Customer customer) => await CreateAsync(customer);


    public async Task DeleteCustomerAsync(int id)
    {
      var customer = await GetByIdAsync(id);
      if (customer != null) await DeleteAsync(customer);
    }
    

    public async Task<Entities.Customer?> GetCustomerByEmailAsync(string email)
    {
      return await FindByCondition(x => x.Email.Equals(email)).SingleOrDefaultAsync();
    }

    public async Task<Entities.Customer?> GetCustomerByIdAsync(int id)
    {
      return await FindByCondition(x => x.Id.Equals(id)).SingleOrDefaultAsync();
    }

    public async Task<Entities.Customer?> GetCustomerByUserNameAsync(string userName)
    {
      return await FindByCondition(x => x.UserName.Equals(userName)).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Entities.Customer>> GetCustomersAsync() => await FindAll().ToListAsync();

    public async Task UpdateCustomerAsync(Entities.Customer customer) => await UpdateAsync(customer);
   
  }
}
