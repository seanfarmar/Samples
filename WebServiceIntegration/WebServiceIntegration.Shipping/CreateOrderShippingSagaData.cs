namespace WebServiceIntegration.Shipping
{
    using System;
    using NServiceBus;

    internal class CreateOrderShippingSagaData : ContainSagaData
    {
        public Guid OrderId { get; set; }
        public string CountryCode { get; set; }
        public Guid CustomerNumber { get; set; }
        public bool ThrowException { get; set; }
    }
}