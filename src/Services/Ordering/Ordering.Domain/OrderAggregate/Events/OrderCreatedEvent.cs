using Contracts.Common.Events;

namespace Ordering.Domain.OrderAggregate.Events
{
    public class OrderCreatedEvent : BaseEvent
    {
        public OrderCreatedEvent(long id, string userName, decimal totalPrice, string documentNo, string emailAddress, string shippingAddress, string invoiceAddress)
        {
            Id = id;
            UserName = userName;
            TotalPrice = totalPrice;
            DocumentNo = documentNo;
            EmailAddress = emailAddress;
            ShippingAddress = shippingAddress;
            InvoiceAddress = invoiceAddress;
        }

        public long Id { get; private set; }
        public string UserName { get; private set; }
        public decimal TotalPrice { get; private set; }
        public string DocumentNo { get; private set; }
        public string EmailAddress { get; private set; }
        public string ShippingAddress { get; private set; }
        public string InvoiceAddress { get; private set; }
    }
}
