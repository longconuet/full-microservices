using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderDbContextSeed
    {
        private readonly ILogger _logger;
        private readonly OrderDbContext _dbContext;

        public OrderDbContextSeed(ILogger logger, OrderDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_dbContext.Database.IsSqlServer())
                {
                    await _dbContext.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            if (!_dbContext.Orders.Any())
            {
                await _dbContext.Orders.AddRangeAsync(
                    new Domain.Entities.Order
                    {
                        UserName = "customer1",
                        FirstName = "customer1",
                        LastName = "customer",
                        EmailAddress = "customer1@gmail.com",
                        ShippingAddress = "Hai Duong",
                        InvoiceAddress = "Ha Noi",
                        TotalPrice = 250
                    });
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
