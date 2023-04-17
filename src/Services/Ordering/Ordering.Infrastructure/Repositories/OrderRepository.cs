using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repositories
{
  public class OrderRepository : RepositoryBase<Order, long, OrderContext>, IOrderRepository
  {
    public OrderRepository(OrderContext dbContext, IUnitOfWork<OrderContext> unitOfWork) : base(dbContext, unitOfWork)
    {
    }

    public async Task<IEnumerable<Order>> GetOrdersByUsername(string username) => await FindByCondition(x => x.UserName.Equals(username)).ToListAsync();

    public async Task<Order> CreateOrder(Order order)
    {
      await CreateAsync(order);
      return order;
    }
  }
}
