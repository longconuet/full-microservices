using Contracts.Common.Interfaces;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Application.Common.Interfaces
{
    public interface IOrderRepository : IRepositoryBaseAsync<Order, long, OrderDbContext>
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
    }
}
