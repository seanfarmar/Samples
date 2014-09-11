namespace WebServiceIntegration.Shipping
{
    using System;
    using Messages.Commands;
    using NServiceBus.Saga;

    class CreateOrderShippingSaga : Saga<CreateOrderShippingSagaData>, IAmStartedByMessages<CreateOrderShipping>
    {
        public void Handle(CreateOrderShipping message)
        {
            Console.WriteLine("Handeling message CreateOrderShipping orderId: {0} OrderNumber: {1}", message.OrderId, message.OrderNumber);

            // do some shipping related logic
            var dispatchOrderToDhl = new DispatchOrderToDhl()
            {
                CountryCode = message.OrderCountryCode,
                OrderId = message.OrderId
            };

            //Dispatch the order to DHL
            Bus.Send(dispatchOrderToDhl);
        }
    }

    internal class CreateOrderShippingSagaData : IContainSagaData
    {
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }
    }
}
