using Contracts.Common.Events;
using Contracts.Domains;
using Ordering.Domain.Enums;
using Ordering.Domain.OrderAggregate.Events;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.Domain.Entities
{
    public class Order : AuditableEventEntity<long>
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

        [NotMapped]
        public string FullName => FirstName + " " + LastName;

        public Order AddedOrder()
        {
            AddDomainEvent(new OrderCreatedEvent(Id, UserName, TotalPrice, "no", EmailAddress, ShippingAddress, InvoiceAddress));
            return this;
        }

        public Order DeletedOrder()
        {
            AddDomainEvent(new OrderDeletedEvent(Id));
            return this;
        }
    }
}
