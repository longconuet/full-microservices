using Microsoft.EntityFrameworkCore;

namespace Product.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
        {
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<TContext>>();
            var context = services.GetService<TContext>();

            try
            {
                logger.LogInformation("Migrating Product - mysql database");
                ExecutingMigrations(context);

                logger.LogInformation("Migrated Product - mysql database");
                InvokeSeeder(seeder, context, services);
            }
            catch (Exception ex)
            {
                logger.LogInformation("An error occured while migrating the Product - mysql database");
            }

            return host;
        }

        public static void ExecutingMigrations<TContext>(TContext context) where TContext : DbContext
        {
            context.Database.Migrate();
        }

        public static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services) where TContext : DbContext
        {
            seeder(context, services);
        }
    }
}
