namespace WebServiceIntegration.Messages.Commands
{
    using System;

    public class DispatchOrderToDhl
    {
        public Guid OrderId { get; set; }
        public Guid DispatchId { get; set; }
        public string CountryCode { get; set; }
        public Guid DhlCustomerNumber { get; set; }
    }
}
