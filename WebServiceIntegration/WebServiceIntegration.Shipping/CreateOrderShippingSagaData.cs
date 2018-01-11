namespace WebServiceIntegration.Shipping
{
    using System;
    using NServiceBus;

    internal class CreateOrderShippingSagaData : ContainSagaData
    {
        public virtual Guid OrderId { get; set; }
        public virtual string CountryCode { get; set; }
        public virtual Guid CustomerNumber { get; set; }
        public virtual bool ThrowException { get; set; }
    }
}