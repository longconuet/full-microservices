using Contracts.Common.Interfaces;
using Customer.API.Persistence;
using Customer.API.Repositories.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Repositories
{
    public class CustomerRepository : RepositoryBaseAsync<Entities.Customer, int, CustomerDbContext>, ICustomerRepository
    {
        public CustomerRepository(CustomerDbContext context, IUnitOfWork<CustomerDbContext> unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<Entities.Customer> GetCustomerByUsernameAsync(string username)
        {
            return await FindByCondition(x => x.UserName.Equals(username)).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Entities.Customer>> GetCustomersAsync()
        {
            return await FindAll().ToListAsync();
        }
    }
}
