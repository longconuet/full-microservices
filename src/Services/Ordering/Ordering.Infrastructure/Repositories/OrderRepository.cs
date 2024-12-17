using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBaseAsync<Order, long, OrderDbContext>, IOrderRepository
    {
        public OrderRepository(OrderDbContext context, IUnitOfWork<OrderDbContext> unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            return await FindByCondition(x => x.UserName.Equals(userName)).ToListAsync();
        }

        public void CreateOrder(Order order)
        {
            Create(order);
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            await UpdateAsync(order);
            return order;
        }

        public async Task DeleteOrder(long id)
        {
            var order = await GetByIdAsync(id);
            if (order != null) 
            {
                await DeleteAsync(order);
            }
        }
    }
}
