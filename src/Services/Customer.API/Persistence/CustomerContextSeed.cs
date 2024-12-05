using Microsoft.EntityFrameworkCore;

namespace Customer.API.Persistence
{
    public static class CustomerContextSeed
    {
        public static IHost SeedCustomerData(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var customerContext = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();
            customerContext.Database.MigrateAsync().GetAwaiter().GetResult();

            CreateCustomer(customerContext, "customer1", "customer1", "customer", "customer1@gmail.com").GetAwaiter().GetResult();
            CreateCustomer(customerContext, "customer2", "customer2", "customer", "customer2@gmail.com").GetAwaiter().GetResult();

            return host;
        }

        private static async Task CreateCustomer(CustomerDbContext customerContext, string username, string firstName, string lastName, string email)
        {
            var customer = await customerContext.Customers.SingleOrDefaultAsync(x => x.UserName.Equals(username));
            if (customer == null) 
            {
                await customerContext.Customers.AddAsync(new Entities.Customer
                {
                    UserName = username,
                    FirstName = firstName,
                    LastName = lastName,
                    EmailAddress = email
                });
                await customerContext.SaveChangesAsync();
            }
        }
    }
}
