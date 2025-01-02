using Inventory.Product.API.Entities;
using MongoDB.Driver;
using Shared.Enums;

namespace Inventory.Product.API.Persistence
{
    public class InventoryDbSeed
    {
        public async Task SeedDataAsync(IMongoClient mongoClient, Inventory.Product.API.Extensions.MongoDatabaseSettings settings)
        {
            var databaseName = settings.DatabaseName;
            var database = mongoClient.GetDatabase(databaseName);
            var inventoryCollection = database.GetCollection<InventoryEntry>("InventoryEntries");
            if (await inventoryCollection.EstimatedDocumentCountAsync() == 0)
            {
                await inventoryCollection.InsertManyAsync(GetPreconfiguredInventoryEntries());
            }
        }

        private IEnumerable<InventoryEntry> GetPreconfiguredInventoryEntries()
        {
            return new List<InventoryEntry>
            {
                new()
                {
                    Quantity = 1,
                    DocumentNo = Guid.NewGuid().ToString(),
                    ItemNo = "Lotus",
                    ExternalDocumentNo = Guid.NewGuid().ToString(),
                    DocumentType = EDocumentType.Purchase
                },
                new()
                {
                    Quantity = 10,
                    DocumentNo = Guid.NewGuid().ToString(),
                    ItemNo = "Cadillac",
                    ExternalDocumentNo = Guid.NewGuid().ToString(),
                    DocumentType = EDocumentType.Purchase
                }
            };
        }
    }
}
