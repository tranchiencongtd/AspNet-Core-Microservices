using Ordering.Domain.Entities;
using Contracts.Common.Interfaces;
using Ordering.Application.Common.Models;

namespace Ordering.Application.Common.Interfaces
{
    public interface IOrderRepository : IRepositoryBase<Order, long>
    {
        Task<IEnumerable<Order>> GetOrdersByUsername(string username);
        Task<Order> CreateOrder(Order order);
    }
}