using Contracts.Common.Interfaces;
using Customer.API.Persistence;

namespace Customer.API.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepositoryQueryAsync<Entities.Customer, int, CustomerDbContext>
    {
        Task<IEnumerable<Entities.Customer>> GetCustomersAsync();
        Task<Entities.Customer> GetCustomerByUsernameAsync(string username);
    }
}
