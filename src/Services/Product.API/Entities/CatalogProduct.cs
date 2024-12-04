using Contracts.Domains;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.API.Entities
{
    public class CatalogProduct : EntityAuditableBase<long>
    {
        [Required]
        [Column(TypeName = "varchar(250)")]
        public string No { get; set; }

        [Required]
        [Column(TypeName = "varchar(250)")]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        public string Summary { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
