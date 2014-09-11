namespace WebServiceIntegration.Messages.Commands
{
    using System;

    public class CreateOrderShipping
    {
        public Guid OrderId { get; set; }
        public string OrderCountryCode { get; set; }
        public int OrderNumber { get; set; }
        public bool ThrowException { get; set; }
    }
}
