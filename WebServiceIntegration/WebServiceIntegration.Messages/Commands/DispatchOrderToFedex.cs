namespace WebServiceIntegration.Messages.Commands
{
    using System;

    public class DispatchOrderToFedex
    {
        public Guid OrderId { get; set; }
        public Guid DispatchId { get; set; }
        public string CountryCode { get; set; }
        public Guid FedexCustomerNumber { get; set; }
        public bool ThrowException { get; set; }
    }
}