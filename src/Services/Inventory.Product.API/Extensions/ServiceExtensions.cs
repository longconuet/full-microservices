using Infrastructure.Extensions;
using MongoDB.Driver;

namespace Inventory.Product.API.Extensions
{
    public static class ServiceExtensions
    {
        internal static IServiceCollection AddConfigurationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseSettings = configuration.GetSection(nameof(MongoDatabaseSettings))
                .Get<MongoDatabaseSettings>();
            services.AddSingleton(databaseSettings);

            return services;
        }

        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));
        }

        private static string GetMongoConnectionString(this IServiceCollection services)
        {
            var settings = services.GetOptions<MongoDatabaseSettings>(nameof(MongoDatabaseSettings));
            if (settings == null || string.IsNullOrEmpty(settings.ConnectionString))
            {
                throw new ArgumentNullException("MongoDatabaseSettings is not configured");
            }

            var databaseName = settings.DatabaseName;
            var mongoDbConnectionString = $"{settings.ConnectionString}/{databaseName}?authSource=admin";
            return mongoDbConnectionString;
        }

        public static void ConfigueMongoDbClient(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient>(
                new MongoClient(GetMongoConnectionString(services)))
                .AddScoped(x => x.GetService<IMongoClient>()?.StartSession());
        }
    }
}
