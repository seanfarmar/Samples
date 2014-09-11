namespace WebServiceIntegration.Messages.Commands
{
    using System;

    public class DispatchOrderToDhlSucsess
    {
        public Guid OrderId { get; set; }
        public Guid DispatchId { get; set; }
        public string CountryCode { get; set; }
        public Guid DhlCustomerNumber { get; set; }
        public bool ThrowException { get; set; }
    }
}
