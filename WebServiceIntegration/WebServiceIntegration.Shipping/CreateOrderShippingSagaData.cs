namespace WebServiceIntegration.Shipping
{
    using System;
    using NServiceBus.Saga;

    internal class CreateOrderShippingSagaData : IContainSagaData
    {
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }
    }
}