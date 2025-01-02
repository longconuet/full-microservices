﻿using Inventory.Product.API.Entities.Abstraction;
using MongoDB.Bson.Serialization.Attributes;
using Shared.Enums;

namespace Inventory.Product.API.Entities
{
    public class InventoryEntry : MongoEntity
    {
        public InventoryEntry()
        {
            DocumentType = EDocumentType.Purchase;
            DocumentNo = Guid.NewGuid().ToString();
            ExternalDocumentNo = Guid.NewGuid().ToString();
        }

        public InventoryEntry(string id) 
        {
            Id = id;
        }

        [BsonElement("quantity")]
        public int Quantity { get; set; }

        [BsonElement("documentType")]
        public EDocumentType DocumentType { get; set; }

        [BsonElement("documentNo")]
        public string DocumentNo { get; set; }

        [BsonElement("externalDocumentNo")]
        public string ExternalDocumentNo { get; set; }

        [BsonElement("itemNo")]
        public string ItemNo { get; set; }
    }
}
