using EventBus.Messages.IntergationEvents.Interfaces;

namespace EventBus.Messages.IntergationEvents.Events
{
    public record BasketCheckoutEvent : IntergrationBaseEvent, IBasketCheckoutEvent
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string InvoiceAddress { get; set; }
    }
}
