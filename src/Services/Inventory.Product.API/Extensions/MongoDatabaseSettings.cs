using Shared.Configurations;

namespace Inventory.Product.API.Extensions
{
    public class MongoDatabaseSettings : DatabaseSettings
    {
        public string DatabaseName { get; set; }
    }
}
