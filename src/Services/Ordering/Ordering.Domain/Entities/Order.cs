using Contracts.Domains;
using Ordering.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.Domain.Entities
{
    public class Order : EntityAuditableBase<long>
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public decimal TotalPrice { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "varchar(250)")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "varchar(250)")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Column(TypeName = "text")]
        public string ShippingAddress { get; set; }

        [Column(TypeName = "text")]
        public string InvoiceAddress { get; set; }

        public EOrderStatus Status { get; set; }
    }
}
