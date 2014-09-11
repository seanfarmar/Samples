namespace WebServiceIntegration.Shipping
{
    using System;
    using Messages.Commands;
    using NServiceBus;
    using NServiceBus.Saga;

    class CreateOrderShippingSaga : Saga<CreateOrderShippingSagaData>, IAmStartedByMessages<CreateOrderShipping>, IHandleMessages<DispatchOrderToDhlFailure>, IHandleMessages<DispatchOrderToDhlSucsess>
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

        public void Handle(DispatchOrderToDhlFailure message)
        {
            // depending on the web service, we can retry
            // notfy on failiure
            // timeout to retry later
            // and so on
        }

        public void Handle(DispatchOrderToDhlSucsess message)
        {
            // complete of mark complete in state to keep the data or rehidrate the saga
        }
    }
}